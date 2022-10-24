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

        private enum eDeepDirection 
        { 
            DeepIn,  
            DeepOut 
        }

        private delegate List<T> SimpleCollector<T>(JSonNode currentNode, List<T> Lista);
        private delegate List<T> StackedCollector<T>(JSonNode currentNode, List<T> Lista, Stack<T> Pila, eDeepDirection eDirection);

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
            Stack<BasicClass> stack = new Stack<BasicClass>();
            //Creamos el Objeto Raiz, de donde colgará toda la estructura de los datos Json.
            stack.Push(new BasicClass("rootObject", null));
            basicClasses.Add(stack.Peek());
            basicClasses = TreeCollector<BasicClass>(_rootNode.Valor, basicClasses, stack, eDeepDirection.DeepIn, BasicClassesExtractor);

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
            {
                Lista = TreeCollector(currentNode.Valor, Lista, func);
            }

            if (currentNode.HermanoMenor != null) // Recorrido por la rama de HermanoMenor
            { 
                Lista = TreeCollector(currentNode.HermanoMenor, Lista, func);
            }

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
        static private List<T> TreeCollector<T>(JSonNode currentNode, List<T> Lista, Stack<T> Pila, eDeepDirection eDeepDirection, StackedCollector<T> func)
        {
            Lista = func(currentNode, Lista, Pila, eDeepDirection); // Ejecutamos la función con los nuevos nodos.

            if (currentNode.Valor != null) // Recorrido por la rama Valor
            {
                Lista = TreeCollector(currentNode.Valor, Lista, Pila, eDeepDirection, func);

                // En este caso volvemos a ejecutar la función para detectar el regreso
                // por la estructura y poder ir vaciando la pila.
                Lista = func(currentNode, Lista, Pila, eDeepDirection.DeepOut);
            }

            if (currentNode.HermanoMenor != null) // Recorrido por la rama de HermanoMenor
            {
                Lista = TreeCollector(currentNode.HermanoMenor, Lista, Pila, eDeepDirection, func);

                // En este caso volvemos a ejecutar la función para detectar el regreso
                // por la estructura y poder ir vaciando la pila.
                Lista = func(currentNode, Lista, Pila, eDeepDirection.DeepOut);
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
        private List<BasicClass> BasicClassesExtractor(JSonNode currentNode, List<BasicClass> Lista, Stack<BasicClass> Pila, eDeepDirection eDeepDirection)
        {
            if (eDeepDirection == eDeepDirection.DeepOut)
            {
                // Haciendo el recorrido de profundidad inversa, hemos llegado al nodo que 
                // ahora está en el top de la pila, así que lo quitamos.
                if(currentNode.LiteralName() == Pila.Peek().Name)
                    Pila.Pop();
            }
            else
            {
                BasicClass parentClass = Pila.Peek();

                // Se ha encontrado un array de objetos.
                if (currentNode.Valor != null && IsAGroupOfObjects(currentNode))
                {
                    BasicClass bc = new BasicClass(currentNode.LiteralName(), parentClass);
                    parentClass.insideClasses.Add(bc);
                    Pila.Push(bc);
                }

                if (IsAnObject(currentNode))
                {
                    BasicClass bc = new BasicClass(currentNode.LiteralName(), parentClass);
                    parentClass.insideClasses.Add(bc);
                    Pila.Push(bc);
                }

                if (currentNode.NodeType == JSonNode.eNodeType.Property)
                {
                    Property newProp = new Property(parentClass, currentNode.LiteralName(), currentNode.LiteralValue(), "string", ",");
                    Pila.Peek().Properties.Add(newProp);
                }
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
