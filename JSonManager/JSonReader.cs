using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JSonManager
{

    public class JSonReader
    {
        private enum eReadingState
        {
            none,
            BeginningOfObject,
            BeginningOfArray,
            BeginningOfExpression,
            WaitingForItem,
            WaitingForValue,
            EndOfExpression,
            EndOfArray,
            EndOfObject
        }

        private const string OnlyInExpressions = "\n\r";
        private const string Separators = ",:";
        private const string Groupers = "[]{}";
        private const char StringEnclouser = '\"';
        private string Markers = Separators + Groupers + StringEnclouser;

        public JSonNode RootNode { get; private set; }

        private eReadingState readingState { get; set; }

        public string fileName { get; set; }

        private Stack<KeyValuePair<char, JSonNode>> jSonNodes { get; set; }

        private JSonNode previousNode { get; set; }
        private JSonNode currentNode { get; set; }


        public JSonReader(string fileName)
        {
            this.fileName = fileName;
        }

        public JSonNode Read()
        {
            readingState = eReadingState.none;
            previousNode = null;
            currentNode = null;

            jSonNodes = new Stack<KeyValuePair<char, JSonNode>>();

            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while (sr.Peek() >= 0)
                    {
                        char currentChar = (char)sr.Read();

                        if (IsAMarker(currentChar))
                        {
                            if (IsAGrouper(currentChar)) { ManageGrouper(currentChar); }

                            if (IsASeparator(currentChar)) { ManageSeparator(currentChar); }

                            if (IsAStringEnclouser(currentChar)) { ManageStringQuote(currentChar); }
                        }
                        else
                        {
                            //Evitamos poner espacios o saltos de linea procedentes del JSon si no estamos leyendo un valor.
                            if (OnlyInExpressions.Contains(currentChar))
                            {
                                if (readingState == eReadingState.BeginningOfExpression)
                                    currentNode.Append(currentChar);
                            }
                            else
                            {
                                currentNode.Append(currentChar);
                            }
                        }
                    }
                }

                RootNode = currentNode;

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            return RootNode;
        }

        #region Quotation Marks

        private void ManageStringQuote(char currentChar)
        {
            // Puede que la comilla esté dentro de una expresión. Si es así, no se tiene encuenta como límite de cadena.
            if (!currentNode.Content.EndsWith(@"\"))
            {

                // Si en la pila había unas comillas y llegan otras, se ha cerrado la expresión.
                if (jSonNodes.Peek().Key == currentChar)
                {
                    readingState = eReadingState.EndOfExpression;
                    jSonNodes.Pop();
                }

                // Si en la pila no había unas comillas y llegan, se ha abierto una expresión.
                else
                {
                    readingState = eReadingState.BeginningOfExpression;
                    jSonNodes.Push(new KeyValuePair<char, JSonNode>('"', currentNode));
                }
            }

            currentNode.Append(currentChar);
        }

        #endregion

        #region Separators

        private void ManageSeparator(char currentChar)
        {
            if (readingState == eReadingState.BeginningOfExpression)
                currentNode.Append(currentChar);
            else
            {
                if (currentChar == ':')
                    ManageValueAssignation();

                if (currentChar == ',')
                    ManageNextItem();
            }
        }

        private void ManageNextItem()
        {
            readingState = eReadingState.WaitingForItem;
            currentNode.Append(',');
            SetNextNode();
        }

        private void ManageValueAssignation()
        {
            readingState = eReadingState.WaitingForValue;
            currentNode.NodeType = JSonNode.eNodeType.Property;
            currentNode.Append(':');
        }

        #endregion

        #region Classification of char

        private bool IsAGrouper(char c)
        {
            return Groupers.Contains(c);
        }

        private bool IsASeparator(char c)
        {
            return Separators.Contains(c);
        }

        private bool IsAStringEnclouser(char c)
        {
            return StringEnclouser.Equals(c);
        }

        private bool IsAMarker(char c)
        {
            return Markers.Contains(c);
        }

        #endregion

        #region Groupers

        /// <summary>
        /// Realiza una gestión pormenorizada dependiendo del elemento de agrupación que se le pase.
        /// </summary>
        /// <param name="currentChar">Símbolo de agrupación según el standar Json.</param>
        private void ManageGrouper(char currentChar)
        {
            if (currentChar.Equals('{'))
                ManageBeginningOfObject();

            if (currentChar.Equals('}'))
                ManageEndingOfObject();

            if (currentChar.Equals('['))
                ManageBeginningOfArray();

            if (currentChar.Equals(']'))
                ManageEndingOfArray();
        }

        #region Managing Array

        /// <summary>
        /// Realiza las acciones pertinentes para gestionar el símbolo de apertura de corchetes "[".
        /// El símbolo "[" se usa en JSon para el comienzo en la definición de un array o conjunto 
        /// de valores que pertenecen a una misma descripción.
        /// </summary>
        private void ManageBeginningOfArray()
        {
            readingState = eReadingState.BeginningOfArray;
            currentNode.NodeType = JSonNode.eNodeType.Array;

            currentNode.Append('[');

            // Agregamos el nodo a la pila para posterior cierre.
            jSonNodes.Push(new KeyValuePair<char, JSonNode>('[', currentNode));

            SetFocusOnValueNode();
        }


        /// <summary>
        /// Realiza las acciones pertinentes para gestionar el símbolo de cierre de corchetes "]".
        /// El símbolo "]" se usa en JSon para finalizar la definición de un array o conjunto 
        /// de valores que pertenecen a una misma descripción.
        /// </summary>
        private void ManageEndingOfArray()
        {
            readingState = eReadingState.EndOfArray;

            // Agregamos un nodo para marcar la finalización del array.
            SetNextNode();
            currentNode.Append(']');

            // Recuperamos el nodo que inicio el objeto para sacarlo de la pila.
            if (jSonNodes.Peek().Key == '[')
                currentNode = jSonNodes.Pop().Value;

            // Colocamos el foco en el siguiente nodo HermanoMenor.
            SetNextNode();
        }

        #endregion


        #region Managing Object

        /// <summary>
        /// Realiza las acciones pertinentes para gestionar el símbolo de apertura de llaves "{".
        /// El símbolo "{" se usa en JSon para el comienzo en la definición de un objeto.
        /// </summary>
        private void ManageBeginningOfObject()
        {
            if (readingState == eReadingState.none)
                currentNode = new JSonNode();

            readingState = eReadingState.BeginningOfObject;
            currentNode.NodeType = JSonNode.eNodeType.Object;

            currentNode.Append('{');

            // Agregamos el nodo a la pila para posterior cierre.
            jSonNodes.Push(new KeyValuePair<char, JSonNode>('{', currentNode));

            SetFocusOnValueNode();
        }

        /// <summary>
        /// Realiza las acciones pertinentes para gestionar el símbolo de cierre de llaves "}".
        /// El símbolo "}" se usa en JSon para finalizar la definición de un objeto.
        /// </summary>
        private void ManageEndingOfObject()
        {
            readingState = eReadingState.EndOfObject;

            // Agregamos un nodo para marcar la finalización del objeto.
            SetNextNode();            
            currentNode.Append('}');

            // Recuperamos el nodo que inicio el objeto para sacarlo de la pila.
            if (jSonNodes.Peek().Key == '{')
                currentNode = jSonNodes.Pop().Value;

            // Comprobamos que no estamos al final del Json.
            if(jSonNodes.Count != 0)
                // Colocamos el foco en el siguiente nodo HermanoMenor.
                SetNextNode();
        }

        #endregion

        #endregion

        #region Managing Nodes

        /// <summary>
        /// Pasamos el foco del nodo actual a su nodo valor.
        /// </summary>
        private void SetFocusOnValueNode()
        {
            // Creamos el nodo valor.
            currentNode.Valor = new JSonNode();

            // Guardamos la referencia del actual nodo.
            previousNode = currentNode;

            // Ponemos el foco en el nodo valor.
            currentNode = currentNode.Valor;
        }

        /// <summary>
        /// Pasamos el foco del nodo actual al siguiente nodo (HermanoMenor).
        /// </summary>
        private void SetNextNode()
        {
            // Creamos el nodo HermanoMenor.
            currentNode.HermanoMenor = new JSonNode();

            // Guardamos la referencia del actual nodo.
            previousNode = currentNode;

            // Ponemos el foco en el nodo HermanoMenor.
            currentNode = currentNode.HermanoMenor;
        }

        #endregion
    }
}
