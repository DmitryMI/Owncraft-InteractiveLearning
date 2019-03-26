using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntLearnShared.Utils;

namespace IntLearnShared.Core.LearningTasks
{
    class SimpleSummEquation : LearningTask
    {
        private int _correctAnswer;

        public SimpleSummEquation()
        {
            IsRandomizable = true;

            GetRandomData();
        }

        private void GetRandomData()
        {
            int a = OwcRandom.GetRandom(0, 20);
            int b = OwcRandom.GetRandom(0, 20);

            _correctAnswer = a + b;

            TaskText = $"Чему равна сумма двух чисел: {a} и {b}?";
        }

        public override void Randomize()
        {
            GetRandomData();
        }

        public override bool CheckAnswer(string answer)
        {
            bool parsed = Int32.TryParse(answer, out var answerInt);

            return parsed && (_correctAnswer == answerInt);
        }

    }
}
