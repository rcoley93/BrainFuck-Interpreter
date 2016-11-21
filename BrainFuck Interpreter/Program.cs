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
                next = null;
            }

            public string CellValue()
            {
                return Convert.ToString((char)value);
            }

            public void setValue(int v)
            {
                value = v;
            }

            override
            public string ToString()
            {
                return "Cell[" + Convert.ToString(pos) + "] = " + Convert.ToString(value);
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

        public void CellInput(int v)
        {
            CurrentCell.setValue(v);
        }

        public int getPos()
        {
            return CurrentCell.pos;
        }
    }

    class Constants
    {
        public const string ShiftLeft   = "<";
        public const string ShiftRight  = ">";
        public const string Output      = ".";
        public const string Input       = ",";
        public const string BeginLoop   = "[";
        public const string EndLoop     = "]";
        public const string Increase    = "+";
        public const string Decrease    = "-";
        public const int    NoPosChange = -1 ;
    }
    
    class Program
    {
        static CellArray MainArray = new CellArray();
        

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("USAGE: BFI.exe input_file");
                return;
            }
                      try

            {
                // Read the stream to a string, and write the string to the console.
                string line = File.ReadAllText(args[0]);
                    int StartLoopCount = 0, EndLoopCount = 0;
                    foreach(char c in line)
                    {
                        if (c.Equals(Constants.BeginLoop)) StartLoopCount++;
                        else if (c.Equals(Constants.EndLoop)) EndLoopCount++;
                    }

                    if(StartLoopCount!= EndLoopCount)
                    {
                        Console.WriteLine("Error: Loop ([ or ]) count doesn't match up!");
                        return;
                    }

                    string[] charArray = line.Split();
                    
                    for(int i = 0;i<charArray.Length;i++)
                    {
                        string s = charArray[i];
                        i = ProcessCommand(s,line,i);            
                    }
            }
            catch (Exception e)
            {
                Console.Write("The file could not be read: ");
                Console.WriteLine(e.Message);
            }
        }

        static int ProcessCommand(string s,string line,int pos)
        {
           // Console.WriteLine("Process Command[{1}]: {0}" , s ,pos);
            switch (s)
            {
                case Constants.ShiftLeft:
                    MainArray.ShiftLeft();
                    break;
                case Constants.ShiftRight:
                    MainArray.ShiftRight();
                    break;
                case Constants.Output:
                    MainArray.PrintCellValue();
                    break;
                case Constants.Increase:
                    MainArray.Increase();
                    break;
                case Constants.Decrease:
                    MainArray.Decrease();
                    break;
                case Constants.Input:
                    MainArray.CellInput((int)Console.ReadKey().KeyChar);
                    break;
                case Constants.BeginLoop:
                    return ProcessLoop(pos, line);
                default:
                    break;
            }
            return pos;
        }

        static int ProcessLoop(int pos,string line)
        {
            string[] charArray = line.Split();
            int endLoopPos = pos, BeginLoopCount = 1, EndLoopCount = 0;
            while (BeginLoopCount != EndLoopCount)
            {
                endLoopPos++;
                if (charArray[endLoopPos].Equals(Constants.EndLoop)) EndLoopCount++;
                if (charArray[endLoopPos].Equals(Constants.BeginLoop)) BeginLoopCount++;
            }
            while(true)
            {
                for(int i = pos + 1; i < endLoopPos; i++)
                {
                    i = ProcessCommand(charArray[i], line, i);
                }
                if (MainArray.GetValue() == 0) break;
            }
            return endLoopPos;
        }
    }
}
