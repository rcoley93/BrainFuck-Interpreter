using System;
using System.IO;

namespace BrainFuck_Interpreter
{
    class CellArray
    {
        private cell CurrentCell;

        public CellArray()
        {
            CurrentCell = new cell(null, 0);
        }
        
        class cell
        {
            public int value,pos;
            public cell next, prev;

            public cell(cell p,int position)
            {
                value = 0;
                pos = position;
                prev = p;
            }

            override
            public string ToString()
            {
                return Convert.ToChar(value).ToString();
            }
        }

        public void ShiftRight()
        {
            if (CurrentCell.next == null) CurrentCell.next = new cell(CurrentCell, CurrentCell.pos + 1);
            CurrentCell = CurrentCell.next;
        }

        public void ShiftLeft()
        {
           if (CurrentCell.prev != null) CurrentCell = CurrentCell.prev; ;
        }

        public void Increase()
        {
            CurrentCell.value++;
        }

        public void Decrease()
        {
            CurrentCell.value--;
        }

        public void PrintCellValue()
        {
            Console.Write(CurrentCell.ToString());
        }

        public int GetValue()
        {
            return CurrentCell.value;
        }
    }

    class Constants
    {
        public const string ShiftLeft   = ">";
        public const string ShiftRight  = "<";
        public const string Output      = ".";
        public const string Input       = ",";
        public const string BeginLoop   = "[";
        public const string EndLoop     = "]";
        public const string Add         = "+";
        public const string Subtract    = "-";
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("USAGE: BFI.exe input_file");
                return;
            }

            CellArray Main = new CellArray();

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(args[0]))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
