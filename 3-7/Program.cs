using System.Reflection;

class Program
{
    class HanoiTowers
    {
        private int _disks;                 //количество этажей
        private int counter;                //счетчик этапов выполнения
        private Stack<int> first;           //первая башня
        private Stack<int> second;          //вторая башня
        private Stack<int> third;           //третья башня

        /// <summary>
        /// В конструкторе инициализируем три стека, первый стек 
        /// заполняем элементами в количестве от 3 до 8
        /// </summary>
        /// <param name="disks">количество элементов</param>
        public HanoiTowers(int disks)
        {
            first = new Stack<int>();                               
            second = new Stack<int>();
            third = new Stack<int>();

            counter = 1;                                            //счетчик начинает свой ход с первого шага
            _disks = disks < 3 ? 3 : disks > 8 ? 8 : disks;         //количество элементов от 3 до 8

            for (int i = 1; i <= _disks; i++)
            {
                first.Push(i);                                      //заполнениеи первого стека
            }
        }
        /// <summary>
        /// Решение задачи с Ханойскими башнями
        /// </summary>
        public void Solution()
        {
            while (second.Count != _disks && third.Count != _disks)     //пока 2я или 3я башня не завершатся
            {
                // 1ю башню сравниваем со 2й, затем 1ю сравниваем с 3й,
                // и наконец 2ю сравниваем с 3й (1<2 = 1->2, 2>1 = 2->1 etc)
                // охранные условия не позволяют возникнуть исключению,
                // возникающему при обращении к пустому стеку (Stack empty)

                if (first.Any() && (!second.Any() || second.Peek() < first.Peek())) { second.Push(first.Pop()); }
                else first.Push(second.Pop());

                Console.WriteLine($"Шаг {counter}  1ю башню сравниваем со 2й башней\n"); counter++;
                PrintAll();     //вывод прогресса в консоль

                if (second.Count == _disks || third.Count == _disks) { break; }
                else if (first.Any() && (!third.Any() || third.Peek() < first.Peek())) { third.Push(first.Pop()); }
                else first.Push(third.Pop());

                Console.WriteLine($"Шаг {counter}  1ю башню сравниваем с 3й башней\n"); counter++;
                PrintAll();     //вывод прогресса в консоль

                if (second.Count == _disks || third.Count == _disks) { break; }
                else if (second.Any() && (!third.Any() || third.Peek() < second.Peek())) { third.Push(second.Pop()); }
                else if (third.Any()) { second.Push(third.Pop()); }

                Console.WriteLine($"Шаг {counter}  2ю башню сравниваем с 3й башней\n"); counter++;
                PrintAll();     //вывод прогресса в консоль
            }
        }
        /// <summary>
        /// вывод состояния отдельной пирамиды с переходом на новую строку
        /// </summary>
        /// <param name="pile"></param>
        private void Print(string name, Stack<int> pile)
        {
            Console.Write($"{name}\t");
            foreach (int item in pile) Console.Write($"{item} ");
            Console.WriteLine();
        }
        /// <summary>
        /// вывод состояния всех пирамид с переходом на новую строку
        /// </summary>
        public void PrintAll()
        {
            Print("Первая башня:", first);
            Print("Вторая башня:", second);
            Print("Третья башня:", third);
            Console.WriteLine();
        }
    }
    static void Main(string[] args)
    {
        Console.Write("Введите в консоль количество этажей башни от 3 до 8 включительно: ");
        int stages;
        bool getInput = int.TryParse(Console.ReadLine(), out stages);
        if (!getInput) Console.WriteLine("Неверное значение. Будет выбрано значение по умолчанию, равное трем.\n");

        HanoiTowers hanoiTower = new HanoiTowers(stages);
        
        Console.WriteLine("Первоначальный вид пирамиды: нумерация начинается " +
            "с единицы, где 1 - фундаментальная часть пирамиды, а все числа " +
            "старше являются ее этажами с сужением к верху. На какой именно башне " +
            "завершится пирамида - на третьей или второй - не имеет значения.\n");

        hanoiTower.PrintAll();                         
        
        hanoiTower.Solution();                          
    }
}