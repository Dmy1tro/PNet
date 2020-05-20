using PNet_Pz_1.Models;
using System;

namespace PNet_Pz_1.Tasks
{
    public class Chaining
    {
        public delegate void ModifyPerson(Person person);

        public void Example()
        {
            ModifyPerson chaining;

            ModifyPerson ch1 = CopyPersonInfo;
            ModifyPerson ch2 = ProcessPersonInfo;
            ModifyPerson ch3 = SavePersonInfo;
            ModifyPerson ch4 = SendPersonInfo;

            chaining = ch1;
            chaining += ch2;
            chaining += ch3;
            chaining += ch4;

            var person = new Person();

            chaining(person);

            Console.Write("---Invocation list---");

            for (int i = 0; i < chaining.GetInvocationList().Length; i++)
            {
                switch (i)
                {
                    case 1: ch1(person);
                        break;

                    case 3: ch3(person);
                        break;

                    default:
                        break;
                }
            }
        }

        public void ProcessPersonInfo(Person person)
        {
            Console.WriteLine("Processing information...");
        }

        public void SavePersonInfo(Person person)
        {
            Console.WriteLine("Saving information");
        }

        public void SendPersonInfo(Person person)
        {
            Console.WriteLine("Sending information");
        }

        public void CopyPersonInfo(Person person)
        {
            Console.WriteLine("Copy information");
        }
    }
}
