using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib;

namespace TestGenerator
{
    internal class Program
    {
        // сначала решаем, сколько вершин
        // все наполняем ребрами, начиная от первой до конца
        // при этом новое ребро соединяет текущую вершину с незаполненной
        // все ребра кратны двум
        // потом либо добавляем одно случайное ребро, либо вычитаем
        // потом выводим граф

        private static void Main()
        {
            var rand = new Random();            // генератор
            var num = rand.Next(4, 13);         // будем генерировать графы от 4 до 12 вершин включительно

            // инициализация
            var graph = new List<int>[num];
            for (var i = 0; i < num; i++)
                graph[i] = new List<int>();

            // заполнение
            for (var i = 0; i < num; i++)
            {
                int numInV;

                if (graph[i].Count == 0) numInV = rand.Next(1, 4) * 2;
                else numInV = graph[i].Count % 2 == 0 ? graph[i].Count : graph[i].Count + 1;

                while (graph[i].Count != numInV)
                {
                    var added = rand.Next(i, num);      // генерируем случайный элемент из последующих

                    graph[i].Add(added);                // добавляем ребро
                    graph[added].Add(i);                // к обеим вершинам
                }
            }

            // решение
            var del = rand.Next(0, 2) > 0;
            var delVertex = rand.Next(0, num);          // первая вершина, для кт добавляем\убираем

            // удаление или добавление
            if (del)                                    // если удаляем, то
            {
                // выбираем вершину точную, которую удаляем, из предложенных
                var delEdge = graph[delVertex][rand.Next(0, graph[delVertex].Count)];

                // удаляем из графа ребро
                graph[delVertex].Remove(delEdge);
                graph[delEdge].Remove(delVertex);
            }
            else
            {
                // выбираем вторую вершину, такую, чтобы не совпадала с первой
                var addEdge = rand.Next(0, num);
                while (addEdge == delVertex) addEdge = rand.Next(0, num);

                // добавляем
                graph[delVertex].Add(addEdge);
                graph[addEdge].Add(delVertex);
            }

            // вывод графа
            Console.WriteLine("Вершин в графе: {0}\n", num);
            for (int i = 0; i < num; i++)
            {
                foreach (var edge in graph[i])
                    Console.Write(edge + " ");

                Console.WriteLine();
            }

            OC.Stay();
        }
    }
}
