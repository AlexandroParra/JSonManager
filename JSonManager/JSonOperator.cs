using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonManager
{
    public class JSonOperator
    {
        private readonly JSonNode _rootNode;

        private delegate List<T> SimpleCollector<T>(JSonNode currentNode, List<T> Lista);
        private delegate List<T> StackedCollector<T,J>(JSonNode currentNode, List<T> Lista, Stack<J> Pila);

        public JSonOperator(JSonNode rootNode)
        {
            _rootNode = rootNode;
        }


        public List<string> GetGroupsOfClasses()
        {
            List<string> groups = TreeCollector<string>(_rootNode, new List<string>(), ClassesDetector);

            return groups;
        }

        public List<BasicClass> GetBasicClasses()
        {
            List<BasicClass> basicClasses = new List<BasicClass>();            
            Stack<KeyValuePair<string, BasicClass>> stack = new Stack<KeyValuePair<string, BasicClass>>();
            //Creamos el Objeto Raiz, de donde colgará toda la estructura de los datos Json.
            stack.Push(new KeyValuePair<string, BasicClass>("{", new BasicClass("rootObject", null)));
            basicClasses.Add(stack.Peek().Value);
            basicClasses = TreeCollector<BasicClass, KeyValuePair<string, BasicClass>>(_rootNode.Valor, basicClasses, stack, BasicClassesExtractor);

            return basicClasses;
        }


        /// <summary>
        /// Método recursivo que prioriza el recorrido por la rama valor frente a la rama hermanoMenor.
        /// </summary>
        /// <typeparam name="T">Tipo de la lista que se devuelve por parámetros.</typeparam>
        /// <param name="currentNode">Nodo actual.</param>
        /// <param name="Lista">Lista de los valores que se devuelven.</param>
        /// <param name="func">Función que recibe por parámetros Nodo Actual y una lista de valores y realiza una evaluación
        /// con la que, presuntamente, agrega un elemento a la lista.</param>
        /// <returns>Devuelve una lista de valores que han cumplido la lógica de <paramref name="func"/>. </returns>
        static private List<T> TreeCollector<T>(JSonNode currentNode, List<T> Lista, SimpleCollector<T> func)
        {
            Lista = func(currentNode, Lista); // Ejecutamos la función con los nuevos nodos.

            if (currentNode.Valor != null) // Recorrido por la rama Valor
                Lista = TreeCollector(currentNode.Valor, Lista, func);

            if (currentNode.HermanoMenor != null) // Recorrido por la rama de HermanoMenor
                Lista = TreeCollector(currentNode.HermanoMenor, Lista, func);

            return Lista;
        }


        /// <summary>
        /// Método recursivo que prioriza el recorrido por la rama valor frente a la rama hermanoMenor.
        /// </summary>
        /// <typeparam name="T">Tipo de la lista que se devuelve por parámetros.</typeparam>
        /// <param name="currentNode">Nodo actual.</param>
        /// <param name="Lista">Lista de los valores que se devuelven.</param>
        /// <param name="Pila">Pila de objetos que se le pasa a la función que realiza las evaluaciones.</param>
        /// <param name="func">Función que recibe por parámetros Nodo Actual y una lista de valores y realiza una evaluación
        /// con la que, presuntamente, agrega un elemento a la lista.</param>
        /// <returns>Devuelve una lista de valores que han cumplido la lógica de <paramref name="func"/>. </returns>
        static private List<T> TreeCollector<T,J>(JSonNode currentNode, List<T> Lista, Stack<J> Pila, StackedCollector<T,J> func)
        {
            Lista = func(currentNode, Lista, Pila); // Ejecutamos la función con los nuevos nodos.

            if (currentNode.Valor != null) // Recorrido por la rama Valor
                Lista = TreeCollector(currentNode.Valor, Lista, Pila, func);

            if (currentNode.HermanoMenor != null) // Recorrido por la rama de HermanoMenor
                Lista = TreeCollector(currentNode.HermanoMenor, Lista, Pila, func);

            return Lista;
        }



        /// <summary>
        /// Añade un elemento a <paramref name="Lista"/> si <paramref name="currentNode"/>
        /// contiene una definición de objeto o la definición de un grupo de objetos.
        /// </summary>
        /// <param name="currentNode">Nodo, extraido una cadena formato Json, que se evalua.</param>
        /// <param name="Lista">Lista que contendrá el nombre de objeto o grupo de objetos.</param>
        /// <returns>Devuelve <paramref name="Lista"/> con un elemento más, si <paramref name="currentNode"/>
        /// cumple con las condiciones necesarias.</returns>
        private List<BasicClass> BasicClassesExtractor(JSonNode currentNode, List<BasicClass> Lista, Stack<KeyValuePair<string, BasicClass>> Pila)
        {
            BasicClass parentClass = Pila.Peek().Value;

            //Se ha encontrado un array de objetos.
            if (currentNode.Valor != null && IsAGroupOfObjects(currentNode))
            {
                BasicClass bc = new BasicClass(currentNode.LiteralName(), parentClass);
                parentClass.insideClasses.Add(bc);
                Pila.Push(new KeyValuePair<string, BasicClass>("[",bc));
            }

            if (IsAnObject(currentNode))
            {
                BasicClass bc = new BasicClass(currentNode.LiteralName(), parentClass);
                parentClass.insideClasses.Add(bc);
                Pila.Push(new KeyValuePair<string, BasicClass>("{", bc));
            }

            if (currentNode.NodeType == JSonNode.eNodeType.Property)
            {
                Property newProp = new Property(parentClass, currentNode.LiteralName(), currentNode.LiteralValue(), "string", ":");
                Pila.Peek().Value.Properties.Add(newProp);
            }

            if ((currentNode.NodeType == JSonNode.eNodeType.EndOfArray && Pila.Peek().Key =="[") ||                     
                (currentNode.NodeType == JSonNode.eNodeType.EndOfObject && Pila.Peek().Key == "{"))
            {
                Pila.Pop();
            }

            return Lista;
        }


        /// <summary>
        /// Añade un elemento a <paramref name="Lista"/> si <paramref name="currentNode"/>
        /// contiene una definición de objeto o la definición de un grupo de objetos.
        /// </summary>
        /// <param name="currentNode">Nodo, extraido una cadena formato Json, que se evalua.</param>
        /// <param name="Lista">Lista que contendrá el nombre de objeto o grupo de objetos.</param>
        /// <returns>Devuelve <paramref name="Lista"/> con un elemento más, si <paramref name="currentNode"/>
        /// cumple con las condiciones necesarias.</returns>
        private List<string> ClassesDetector(JSonNode currentNode, List<string> Lista)
        {            
            if (currentNode.Valor != null && IsAGroupOfObjects(currentNode))
                Lista.Add(currentNode.LiteralName());

            if (IsAnObject(currentNode))
                Lista.Add(currentNode.LiteralName());

            return Lista;
        }

        private bool IsAnObject(JSonNode currentNode)
        {
            return currentNode.NodeType == JSonNode.eNodeType.Object;
        }

        private bool IsAGroupOfObjects(JSonNode currentNode)
        {
            return currentNode.NodeType == JSonNode.eNodeType.Array
                && currentNode.Valor.NodeType == JSonNode.eNodeType.Object;
        }
    }
}
