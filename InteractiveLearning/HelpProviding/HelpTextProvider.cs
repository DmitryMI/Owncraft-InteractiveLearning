using System.Collections;
using System.Collections.Generic;

namespace InteractiveLearning.HelpProviding
{
    class HelpTextProvider
    {
        private static HelpTextProvider _instance;

        public static HelpTextProvider GetInstance()
        {
            return _instance ?? (_instance = new HelpTextProvider());
        }

        private bool _helpLoaded;

        private List<string> _termList;
        private List<string> _termDataList;

        private void LoadPrebuiltHelp()
        {
            List<string> terms = new List<string>()
            {
                "Сумма",
                "Вычитание",
                "Деление",
                "Интеграл"
            };

            List<string> data = new List<string>()
            {
                "Сумма -  в математике это результат операции" +
                " сложения числовых величин (чисел, функций, векторов, " +
                "матриц и т. д.), либо результат последовательного " +
                "выполнения нескольких операций сложения (суммирования). ",

                "Вычита́ние (убавление) — одна из вспомогательных бинарных + " +
                "математических операций (арифметических действий) двух аргументов" +
                " (уменьшаемого и вычитаемого), результатом которой является новое " +
                "число (разность), получаемое уменьшением значения первого аргумента" +
                " на значение второго аргумента.",

                "Деле́ние (операция деления) — действие, обратное умножению. " +
                "Подобно тому, как умножение заменяет неоднократно повторенное " +
                "сложение, деление заменяет неоднократно повторенное вычитание. ",

                "Интеграл — одно из важнейших понятий математического анализа, " +
                "которое возникает при решении задач о нахождении площади под кривой," +
                " пройденного пути при неравномерном движении, массы неоднородного тела," +
                " и тому подобных, а также в задаче о восстановлении функции по её " +
                "производной (неопределённый интеграл). Упрощённо интеграл можно " +
                "представить как аналог суммы для бесконечного числа бесконечно малых " +
                "слагаемых. В зависимости от пространства, на котором задана подынтегральная " +
                "функция, интеграл может быть — двойной, тройной, криволинейный, поверхностный " +
                "и так далее; также существуют разные подходы к определению интеграла — " +
                "различают интегралы Римана, Лебега, Стилтьеса и другие. "
            };

            if(_termList == null)
                _termList = new List<string>();
            if(_termDataList == null)
                _termDataList = new List<string>();

            _termList.AddRange(terms);
            _termDataList.AddRange((data));
        }

        private void LoadHelp()
        {
            // Some long heavy actions
            LoadPrebuiltHelp();
        }

        public void EnsureHelpLoaded()
        {
            if (!_helpLoaded)
            {
                LoadHelp();
                _helpLoaded = true;
            }
        }

        public IList<TextPair> GetHelpValues()
        {
            EnsureHelpLoaded();

            List<TextPair> table = new List<TextPair>();

            for (int i = 0; i < _termList.Count; i++)
            {
                table.Add(new TextPair(_termList[i], _termDataList[i]));
            }

            return table;
        }
    }
}
