using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public enum Statee
        {
            first,second,result
        }

        private const string Invalid = "Invalid input";
        string NumberString = "0"; //строковый вид числа
        double NumResult = 0;
        double Memory = 0;
        public Statee State = Statee.first;
        private void Counter(double Num1, double Num2)
        {
            if (BlockDo.Text == "+")
            {
                NumResult = Num1 + Num2;
            }
            else
            if (BlockDo.Text == "-")
            {
                NumResult = Num1 - Num2;
            }
            else
            if (BlockDo.Text == "x")
            {
                NumResult = Num1 * Num2;
            }
            else
            if (BlockDo.Text == "÷")
            {
                NumResult = Num1 / Num2;
            }
        }
        private void Delete()
        {
            Block2.Text = null;
            Block1.Text = null;
            BlockDo.Text = null;
            State = Statee.first;
        }
        private bool CantDo()
        {
            return BlockMain.Text.Equals(Invalid) || (!(BlockMain.Text.ToLower().IndexOf('e') == -1)) || (!(BlockMain.Text.ToLower().IndexOf('∞') == -1));
        }
        private void ButtonNumber(object sender, RoutedEventArgs e)
        { 
            if ((State == Statee.result)||((BlockMain.Text != "")&&(Block1.Text!="")) || CantDo())
            {
                Delete();
                NumberString = "0";
                BlockMain.Text = NumberString;
            }
            if (NumberString == "0")
                NumberString = null;
            NumberString += (sender as Button).Content.ToString();
            if (State==Statee.first)
            {
                BlockMain.Text = NumberString;
            } else
            if ((State == Statee.second)&&(BlockMain.Text==""))
            {
                Block2.Text = NumberString;
            } 


        }

        private void ButtonDot(object sender, RoutedEventArgs e)
        {
            if ((State == Statee.result) || ((BlockMain.Text != "") && (Block1.Text != "")) || CantDo())
            {
                Delete();
                NumberString = "0";
                BlockMain.Text = NumberString;
            }
            if ((String.IsNullOrEmpty(NumberString))||(NumberString=="-")||(NumberString == ""))
            {
                NumberString =NumberString + "0,";
            } else
            if (NumberString.ToLower().IndexOf(',') == -1)
            {
                    NumberString = NumberString + ',';
            }
            if (State == Statee.first)
            {
                BlockMain.Text = NumberString;
            } else 
            if (State == Statee.second)
            {
                Block2.Text = NumberString;
            }
        }

        private void ButtonDo(object sender, RoutedEventArgs e)
        {
            if ((State == Statee.first) && ((BlockMain.Text == "") || (BlockMain.Text == "0")) && (sender as Button).Content.ToString() == "-")
            {
                NumberString = "-";
                BlockMain.Text = NumberString;
            }
            else
            if (CantDo() && (sender as Button).Content.ToString() == "-")
            {
                Delete();
                NumberString = "-";
                BlockMain.Text = NumberString;
            }
            else
            if (!CantDo())
            {
                if (((BlockMain.Text == "-") || (String.IsNullOrEmpty(BlockMain.Text))) && (String.IsNullOrEmpty(Block1.Text)))
                {
                    NumberString = "0";
                    State = Statee.first;
                }
                if (State == Statee.first)
                {
                    Block1.Text = System.Convert.ToString(System.Convert.ToDouble(NumberString));
                    NumResult = System.Convert.ToDouble(NumberString);
                    State = Statee.second;
                }
                if (Block2.Text != "")
                {
                    double Num1 = System.Convert.ToDouble(Block1.Text);
                    double Num2 = System.Convert.ToDouble(Block2.Text);
                    if ((System.Convert.ToString(System.Convert.ToDouble(Block2.Text)) == "0") && (BlockDo.Text == "÷"))
                    {
                        BlockMain.Text = Invalid;
                        Block2.Text = "0";
                        State = Statee.result;
                    }
                    else
                    {
                        Counter(Num1, Num2);
                        Block2.Text = null;
                        Block1.Text = System.Convert.ToString(NumResult);
                        State = Statee.second;
                    }
                }
                if (((State == Statee.result) && !CantDo()) || (BlockMain.Text != ""))
                {
                    Block1.Text = NumResult.ToString();
                    Block2.Text = null;
                    State = Statee.second;
                }

                if (State != Statee.result)
                {
                    BlockMain.Text = null;
                    NumberString = "0";
                    BlockDo.Text = (sender as Button).Content.ToString();
                }
            }
        }

        private void ButtonPercent(object sender, RoutedEventArgs e)
        {
            if (((State == Statee.first) ||(State == Statee.result))&& !CantDo())
            {
                if ((BlockMain.Text == "-") || (BlockMain.Text == "") || (BlockMain.Text == "0"))
                    BlockMain.Text = "0";
                NumberString = System.Convert.ToString(System.Convert.ToDouble(BlockMain.Text) * 0.01);
                BlockMain.Text = NumberString;
            }else
            if (State == Statee.second)
            {
                if ((Block2.Text == "-") || (Block2.Text == ""))
                    Block2.Text = "0";
                double Num2 = 0.01 * System.Convert.ToDouble(Block2.Text);
                if ((BlockDo.Text == "-") || (BlockDo.Text == "+"))
                {
                    NumberString = System.Convert.ToString(System.Convert.ToDouble(Block1.Text)* Num2);
                } else
                if ((BlockDo.Text == "x") || (BlockDo.Text == "÷"))
                    NumberString = System.Convert.ToString(Num2);
                Block2.Text = NumberString;
            }
        }

        private void ButtonEquals(object sender, RoutedEventArgs e)
        {
            if (State != Statee.result)
            {
                if (Block1.Text != "")
                {
                    if (Block2.Text != "")
                    {
                        double Num1 = System.Convert.ToDouble(Block1.Text);
                        double Num2 = System.Convert.ToDouble(Block2.Text);
                        if ((Num2 == 0) && (BlockDo.Text == "÷"))
                        {
                            BlockMain.Text = Invalid;
                            Block2.Text = "0";
                        }
                        else
                        {
                            Counter(Num1, Num2);
                            BlockMain.Text = NumResult.ToString();
                        }

                        State = Statee.result;
                    }
                    else
                    {

                        double Num1 = NumResult;
                        double Num2 = System.Convert.ToDouble(Block1.Text);
                        if ((Num2 == 0) && (BlockDo.Text == "÷"))
                        {
                            BlockMain.Text = Invalid;
                            Block2.Text = "0";
                        }
                        else
                        {
                            Counter(Num1, Num2);
                            Block2.Text = null;
                            BlockMain.Text = System.Convert.ToString(NumResult);
                        }
                    }
                }
                else
                {
                    if ((BlockMain.Text == "-") || (BlockMain.Text == "") || (String.IsNullOrEmpty(BlockMain.Text)))
                    {
                        NumberString = "0";
                    }
                    else
                    {
                        NumberString = System.Convert.ToString(System.Convert.ToDouble(BlockMain.Text));
                    }
                    BlockMain.Text = NumberString;
                }
            } else
            if ((!CantDo() || (!(BlockMain.Text.ToLower().IndexOf('e') == -1))) && (Block1.Text!=""))
            {
                double Num1 = NumResult;
                double Num2 = System.Convert.ToDouble(Block1.Text);
                
                if (Block2.Text != "")
                {
                    Num2 = System.Convert.ToDouble(Block2.Text);
                }

                if ((Num2 == 0) && (BlockDo.Text == "÷"))
                {
                    BlockMain.Text = Invalid;
                    Block2.Text = "0";
                    State = Statee.result;
                }
                else
                {
                    Counter(Num1, Num2);
                    BlockMain.Text = System.Convert.ToString(NumResult);
                }
            }
        }

        private void ButtonC(object sender, RoutedEventArgs e)
        {
            Delete();
            NumberString = "0";
            BlockMain.Text = NumberString;
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            if ((Block2.Text != "") || (BlockMain.Text != ""))
            {
                if (CantDo() || ((BlockMain.Text != "") && (Block1.Text != "")))
                {
                    Delete();
                    NumberString = "0";
                    BlockMain.Text = NumberString;
                }
                if (State == Statee.first)
                {
                    BlockMain.Text = BlockMain.Text.Remove(BlockMain.Text.Length - 1);
                    NumberString = BlockMain.Text;
                    if ((NumberString == "0") || (BlockMain.Text == ""))
                    {
                        BlockMain.Text = "0";
                    }
                    NumberString = BlockMain.Text;
                }
                else
                if (State == Statee.second)
                {
                    Block2.Text = Block2.Text.Remove(Block2.Text.Length - 1);
                    if ((NumberString == "0") || (Block2.Text == ""))
                    {
                        Block2.Text = "0";
                    }
                    NumberString = Block2.Text;
                }
            }
        }

        private void ButtonM(object sender, RoutedEventArgs e)
        {
            if (!CantDo() || (!(BlockMain.Text.ToLower().IndexOf('e') == -1)))
            {
                if ((State == Statee.first) || (State == Statee.result))
                {
                    if ((sender as Button).Content.ToString() == "MR")
                    {
                        Delete();
                        NumberString = System.Convert.ToString(System.Convert.ToDouble(Memory)); ;
                        BlockMain.Text = NumberString;
                    }
                    if ((BlockMain.Text == "-") || (BlockMain.Text == ""))
                        BlockMain.Text = "0";
                    if ((sender as Button).Content.ToString() == "M+")
                        Memory = Memory + System.Convert.ToDouble(BlockMain.Text);
                    if ((sender as Button).Content.ToString() == "M-")
                        Memory = Memory - System.Convert.ToDouble(BlockMain.Text);
                }
                else
                if (State == Statee.second)
                {
                    if ((sender as Button).Content.ToString() == "MR")
                    {
                        NumberString = System.Convert.ToString(Memory);
                        Block2.Text = NumberString;
                    }
                    if ((Block2.Text == "-") || (Block2.Text == ""))
                        Block2.Text = "0";
                    if ((sender as Button).Content.ToString() == "M+")
                        Memory = Memory + System.Convert.ToDouble(Block2.Text);
                    if ((sender as Button).Content.ToString() == "M-")
                        Memory = Memory - System.Convert.ToDouble(Block2.Text);
                }

                if ((sender as Button).Content.ToString() == "MC")
                {
                    Memory = 0;
                }
            }
        }

        private void ButtonSqrt(object sender, RoutedEventArgs e)
        {
            if (!CantDo() || (!(BlockMain.Text.ToLower().IndexOf('e') == -1)))
            {
                if (State == Statee.result)
                {
                    Delete();
                }
                if (State == Statee.first)
                {
                    if ((BlockMain.Text == "-") || (BlockMain.Text == ""))
                        BlockMain.Text = "0";
                    if (System.Convert.ToDouble(BlockMain.Text) < 0)
                    {
                        NumberString = Invalid;
                        State = Statee.result;
                        BlockMain.Text = NumberString;
                    }
                    else
                    {
                        NumberString = System.Convert.ToString(Math.Sqrt(System.Convert.ToDouble(BlockMain.Text)));
                        BlockMain.Text = NumberString;
                    }
                }
                if (State == Statee.second)
                {
                    if ((Block2.Text == "-") || (Block2.Text == ""))
                        Block2.Text = "0";
                    if (System.Convert.ToDouble(Block2.Text) < 0)
                    {
                        NumberString = Invalid;
                        State = Statee.result;
                        Block2.Text = NumberString;
                    }
                    else
                    {
                        NumberString = System.Convert.ToString(Math.Sqrt(System.Convert.ToDouble(Block2.Text)));
                        Block2.Text = NumberString;
                    }
                }
            }
        }

        private void ButtonSqu(object sender, RoutedEventArgs e)
        {
            if (!CantDo() || (!(BlockMain.Text.ToLower().IndexOf('e') == -1)))
            {
                if (State == Statee.result)
                {
                    Delete();
                }
                if (State == Statee.first)
                {
                    if ((BlockMain.Text == "-") || (BlockMain.Text == ""))
                        BlockMain.Text = "0";

                    NumberString = System.Convert.ToString((System.Convert.ToDouble(BlockMain.Text) * (System.Convert.ToDouble(BlockMain.Text))));
                    BlockMain.Text = NumberString;
                }
                if (State == Statee.second)
                {
                    if ((Block2.Text == "-") || (Block2.Text == ""))
                        Block2.Text = "0";
                    NumberString = System.Convert.ToString((System.Convert.ToDouble(Block2.Text)) * (System.Convert.ToDouble(Block2.Text)));
                    Block2.Text = NumberString;
                }
            }
        }

        private void ButtonDevine(object sender, RoutedEventArgs e)
        {
            if (!CantDo() || (!(BlockMain.Text.ToLower().IndexOf('e') == -1)))
            {
                if (State == Statee.result)
                {
                    Delete();
                }
                if (State == Statee.first)
                {
                    if ((BlockMain.Text == "-") || (BlockMain.Text == ""))
                        BlockMain.Text = "0";
                    if (System.Convert.ToDouble(BlockMain.Text) == 0)
                    {
                        NumberString = Invalid;
                        State = Statee.result;
                    }
                    else
                    {
                        NumberString = System.Convert.ToString(1 / (System.Convert.ToDouble(BlockMain.Text)));
                    }
                    BlockMain.Text = NumberString;
                }
                if (State == Statee.second)
                {
                    if ((Block2.Text == "-") || (Block2.Text == ""))
                        Block2.Text = "0";
                    if (System.Convert.ToDouble(Block2.Text) == 0)
                    {
                        NumberString = Invalid;
                        BlockMain.Text = NumberString;
                        Block2.Text = "0";
                        State = Statee.result;
                    }
                    else
                    {
                        NumberString = System.Convert.ToString(1 / (System.Convert.ToDouble(Block2.Text)));
                        Block2.Text = NumberString;
                    }
                }
            }

        }

        private void ButtonSign(object sender, RoutedEventArgs e)
        {
            if (!CantDo() || (!(BlockMain.Text.ToLower().IndexOf('e') == -1)))
            {
                if (State == Statee.result)
                {
                    Delete();
                }
                if (State == Statee.first)
                {
                    if ((BlockMain.Text == "-") || (BlockMain.Text == ""))
                        BlockMain.Text = "0";
                    NumberString = System.Convert.ToString((-1) * (System.Convert.ToDouble(BlockMain.Text)));
                    BlockMain.Text = NumberString;
                }
                if (State == Statee.second)
                {
                    if ((Block2.Text == "-") || (Block2.Text == ""))
                        Block2.Text = "0";
                    NumberString = System.Convert.ToString((-1) * (System.Convert.ToDouble(Block2.Text)));
                    Block2.Text = NumberString;
                }
            }

        }


    }
}
