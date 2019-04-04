namespace TestBotDiscord
{
    static class Control
    {
        //Gelen karakterin rakam olup, olmadığını kontrol ediyor
        public static bool numberControl(char c)
        {
            char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            bool isNumber = false;

            for (int i = 0; i < 10; i++)
            {
                if (c == numbers[i])
                {
                    isNumber = true;
                    break;
                }
            }

            return isNumber;
        }
    }

    //private void OperatorControl(string message)
    //{
    //    char[] Operators = { '<', '=', '>' };

    //    for (int j = 0; j < message.Length; j++)
    //    {
    //        for (int i = 0; i < 3; i++)
    //            if (message[j] == Operators[i])
    //            {
    //                condition += message[j];
    //                if (message[j + 1] != ' ' || !IntControl(message[j + 1]))
    //                {
    //                    condition += message[j + 1];
    //                }
    //                for (int k = j; j < message.Length; j++)
    //                {
    //                    if (IntControl(message[k]))
    //                        conditionNumber += message[k];
    //                }
    //                break;
    //            }
    //        if (condition != "")
    //        {
    //            if (conditionNumber != "")
    //                break;
    //            else
    //                conditionNumber = "0";
    //        }
    //    }
    //}
}
