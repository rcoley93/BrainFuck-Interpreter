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
                this.value = 0;
                this.pos = position;
                this.prev = p;
                this.next = null;
            }
            public string CellValue()
            {
                return Convert.ToString((char)this.value);
            }

            override
            public string ToString()
            {
                return "Cell[" + Convert.ToString(this.pos) + "] = " + Convert.ToString(this.value);
            }
        }

        public void ShiftRight()
        {
            if (CurrentCell.next == null) CurrentCell.next = new cell(CurrentCell, CurrentCell.pos + 1);
            CurrentCell = CurrentCell.next;
        }

        public void ShiftLeft()
        {
           if (CurrentCell.prev != null) CurrentCell = CurrentCell.prev;
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
            Console.Write(CurrentCell.CellValue());
        }

        public int GetValue()
        {
            return CurrentCell.value;
        }

        public void printCellInfo()
        {
            Console.WriteLine(CurrentCell.ToString());
        }
    }

    class Constants
    {
        public const char ShiftLeft   = '<';
        public const char ShiftRight  = '>';
        public const char Output      = '.';
        public const char Input       = ',';
        public const char BeginLoop   = '[';
        public const char EndLoop     = ']';
        public const char Increase    = '+';
        public const char Subtract    = '-';
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
            {   
                using (StreamReader sr = new StreamReader(args[0]))
                {
                    // Read the stream to a string, and write the string to the console.
                    string line = sr.ReadToEnd();
                    foreach(char c in line)
                    {
                        switch (c)
                        {
                            case Constants.ShiftLeft:
                                Main.ShiftLeft();
                                break;
                            case Constants.ShiftRight:
                                Main.ShiftRight();
                                break;
                            case Constants.Output:
                                Main.PrintCellValue();
                                break;
                            case Constants.Increase:
                                Main.Increase();
                                break;
                            default:
                                break;
                        }
                    }
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
