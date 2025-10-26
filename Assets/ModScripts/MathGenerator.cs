using System.Linq;
using static UnityEngine.Random;

public class MathGenerator
{
    private struct TermInfo
    {
        public int Digit { get; private set; }
        public ArithmeticOperator? Arithmetic { get; private set; }

        public TermInfo(int digit, ArithmeticOperator? arithmetic)
        {
            Digit = digit;
            Arithmetic = arithmetic;
        }

        public override string ToString() => Arithmetic != null ? $"{Digit} {"+-"[(int)Arithmetic.Value]}" : Digit.ToString();
    }

    private TermInfo[] terms;
    private int answer;

    public void GenerateProblem(out int answerOutput, out string arithmeticOutput)
    {
        var sets = new[] { 2, 3, 4 };

        var threeNumbersOnly = new[] { 1, 2, 3 };

        var determinedSet = sets.PickRandom();

        do
        {
            terms = Enumerable.Range(0, determinedSet).Select(x => new TermInfo(threeNumbersOnly.PickRandom(), (x == determinedSet - 1 ? (ArithmeticOperator?)null : (ArithmeticOperator)Range(0, 2)))).ToArray();

            ArithmeticOperator? arithOperator = null;

            for (int i = 0; i < terms.Length; i++)
            {
                if (i == 0)
                {
                    answer = terms[i].Digit;
                    arithOperator = terms[i].Arithmetic;
                    continue;
                }

                switch (arithOperator)
                {
                    case ArithmeticOperator.Plus:
                        answer += terms[i].Digit;
                        break;
                    case ArithmeticOperator.Minus:
                        answer -= terms[i].Digit;
                        break;
                }

                arithOperator = terms[i].Arithmetic;
            }
        }
        while (answer < 1 || answer > 3);

        answerOutput = answer;
        arithmeticOutput = terms.Join();
    }

    public override string ToString() => $"{terms.Join()} = {answer}";

}