using Math24.ExtensionMethod;
using Math24.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static Math24.View.Math24Dialog;

namespace Math24.API
{
    public  class MathAPI
    {
        double clearance { get; set; } = 0.0000001;
        private MathAPI() { }
        private static MathAPI _Instance { get; set; } = null;
        public static MathAPI GetInstance()
        {
            _Instance = _Instance ?? new MathAPI();
            return _Instance;
        }

        public double Addition(double one, double two, out string strName)
        {
            strName = "+";
            return one + two;
        }
        public double Subtraction(double one, double two, out string strName)
        {
            strName = "-";
            return one - two;
        }
        public double Multiplication(double one, double two, out string strName)
        {
            strName = "*";
            return one * two;
        }
        public double Division(double one, double two, out string strName)
        {
            strName = "/";
            return one / two;
        }

        public double Caculate(string one, string two, string caculator)
        {
            string caculateTT;
            switch (caculator)
            {
                case "+":
                    {
                        return Addition(double.Parse(one), double.Parse(two), out caculateTT);
                    }
                case "-":
                    {
                        return Subtraction(double.Parse(one), double.Parse(two), out caculateTT);
                    }
                case "*":
                    {
                        return Multiplication(double.Parse(one), double.Parse(two), out caculateTT);
                    }
                case "/":
                    {
                        return Division(double.Parse(one), double.Parse(two), out caculateTT);
                    }

            }
            return -999;

        }

        public int GetMinus()
        {
            Random crandom = new Random();

            return crandom.Next(0, 2) == 1 ? 1 : -1;
        }

        /// <summary>
        /// 把輸入的數額轉換成數字字串
        /// </summary>
        /// <param name="inputLine">數額值</param>
        /// <returns></returns>
        public string MakeNumberWords(string inputLine)
        {
            string newNumberWords = null;
            int inputNumber = int.Parse(inputLine);

            for (int i = 1; i <= inputNumber; i++)
            {
                newNumberWords += i.ToString();
            }
            return newNumberWords;
        }

        public List<List<string>> GetIntListNest(List<List<string>> strNest, Dictionary<string, int> dic)
        {
            foreach (var item in strNest)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    string str = item[i].ToString();
                    item[i] = dic[str].ToString();
                }
            }


            return strNest;
        }      

        public List<List<string>> GetTptalSort(string inputLine, List<List<string>> obj )
        {
            var api = MathAPI.GetInstance();       
            //string inputLine = "abcd";

            //MessageBox.Show(GetPlateFunction(4).ToString());
            // 建立一個 RecursionNumber 類別
            RecursionNumber recNumber = new RecursionNumber(obj);

            int inputNumber;  // 記錄轉換是數字時的數值
            bool NumberOrNot = int.TryParse(inputLine, out inputNumber);  // 利用 TryParse 來辨識是文字還是數字
            if (NumberOrNot)
            {
                string newinputLine = api.MakeNumberWords(inputLine);
                recNumber.InputSet = recNumber.MakeCharArray(newinputLine);  // 把數字帶入
            }
            else
            {
                recNumber.InputSet = recNumber.MakeCharArray(inputLine);  // 把文字帶入
            }

            recNumber.CalcPermutation(0);  // 計算變化次數

            return obj;
        }

        public string GetInputLine(Dictionary<string, int> dic)
        {
            string str = "";
            foreach (var item in dic.Keys)
            {
                str = str + item;

            }
            return str;
        }

        public List<int> CaculateAnswerGlobal(List<string> list)
        {
            List<int> intList = new List<int>();
            list.ForEach(x => { intList.Add(int.Parse(x)); });

            return intList;
        }

        public void CaculateAnswer(List<int> list, List<CaculateAction> CaculateActionList, List<AnsAndFunction> ProbabilitySortList, ListBox listBox2)
        {
            string stt = "";
            for (int i = 0; i < list.Count; i++)
            {
                stt = stt + list[i].ToString();
                if (i != list.Count - 1)
                {
                    stt = stt + ",";
                }
            }

            double tt1 = -999;
            string caculator1 = "";
            string show1 = "";
            foreach (var cu1 in CaculateActionList)
            {
                tt1 = cu1(list[0], list[1], out caculator1);
                show1 = "(" + list[0].CustomToString() + caculator1 + list[1].CustomToString() + ")";

                double tt2 = -999;
                string caculator2 = "";
                string show2 = "";
                foreach (var cu2 in CaculateActionList)
                {
                    tt2 = cu2(tt1, list[2], out caculator2);
                    show2 = "(" + show1 + caculator2 + list[2].CustomToString() + ")";

                    double tt3 = -999;
                    string caculator3 = "";
                    string show3 = "";
                    foreach (var cu3 in CaculateActionList)
                    {
                        tt3 = cu3(tt2, list[3], out caculator3);
                        show3 = "(" + show2 + caculator3 + list[3].CustomToString() + ")";

                        string ss = stt + "-->" + show3 + "=" + tt3;
                        listBox2.Items.Add((object)(ss));
                        ProbabilitySortList.Add(
                            new AnsAndFunction()
                            {
                                sort = stt,
                                Ans = tt3.ToString(),
                                Function = show3,
                                ListShow = ss,
                                intFirst = list[0],
                                strFirst = list[0].ToString(),
                                caculate12 = caculator1.ToString(),
                                intSecond = list[1],
                                strSecond = list[1].ToString(),
                                caculate23 = caculator2.ToString(),
                                intThired = list[2],
                                strThired = list[2].ToString(),
                                caculate34 = caculator3.ToString(),
                                intFourth = list[3],
                                strFourth = list[3].ToString(),
                            });
                    }
                }
            }


        }

        public  void CaculateAdditionAns(TextBox textBox6,ListBox listBox2, ref List<AnsAndFunction> AnsList2)
        {
            List<AnsAndFunction> additionAns = new List<AnsAndFunction>();
            var api = MathAPI.GetInstance();

            #region  //(1*2)-(3/2)
            foreach (var item in AnsList2)
            {
                string ans = api.Caculate(api.Caculate(item.strFirst, item.strSecond, item.caculate12).ToString(),
                    api.Caculate(item.strThired, item.strFourth, item.caculate34).ToString(), item.caculate23).ToString();
                if (System.Math.Abs(double.Parse(ans) - double.Parse(textBox6.Text)) < clearance)
                {
                    ans = textBox6.Text;
                }

                string function = string.Format(
                                "({0}{1}{2}){3}({4}{5}{6})",
                                item.intFirst.CustomToString(), item.caculate12, item.intSecond.CustomToString(),
                                item.caculate23, item.intThired.CustomToString(), item.caculate34, item.intFourth.CustomToString());

                additionAns.Add(
                            new AnsAndFunction()
                            {
                                sort = item.sort,
                                Ans = ans.ToString(),
                                Function = function,
                                ListShow = item.sort + "-->" + function + "=" + ans,
                                strFirst = item.strFirst,
                                caculate12 = item.caculate12,
                                strSecond = item.strSecond,
                                caculate23 = item.caculate23,
                                strThired = item.strThired,
                                caculate34 = item.caculate34,
                                strFourth = item.strFourth,
                            });
            }
            #endregion

            #region //(1*(2-3))/2
            foreach (var item in AnsList2)
            {
                string middle = api.Caculate(item.strSecond, item.strThired, item.caculate23).ToString();
                string fmi = api.Caculate(item.strFirst, middle, item.caculate12).ToString();
                string ans = api.Caculate(fmi, item.strFourth, item.caculate34).ToString();
                if (System.Math.Abs(double.Parse(ans) - double.Parse(textBox6.Text)) < clearance)
                {
                    ans = textBox6.Text;
                }

                string function = string.Format(
                                "({0}{1}({2}{3}{4})){5}{6}",
                                item.intFirst.CustomToString(), item.caculate12, item.intSecond.CustomToString(),
                                item.caculate23, item.intThired.CustomToString(), item.caculate34, item.intFourth.CustomToString());

                additionAns.Add(
                            new AnsAndFunction()
                            {
                                Ans = ans.ToString(),
                                Function = function,
                                ListShow = item.sort + "-->" + function + "=" + ans,
                                strFirst = item.strFirst,
                                caculate12 = item.caculate12,
                                strSecond = item.strSecond,
                                caculate23 = item.caculate23,
                                strThired = item.strThired,
                                caculate34 = item.caculate34,
                                strFourth = item.strFourth,
                            });
            }
            #endregion

            #region//1*((2-3)/2)
            foreach (var item in AnsList2)
            {
                string middle = api.Caculate(item.strSecond, item.strThired, item.caculate23).ToString();
                string lmi = api.Caculate(middle, item.strFourth, item.caculate34).ToString();
                string ans = api.Caculate(item.strFirst, lmi, item.caculate12).ToString();
                if (System.Math.Abs(double.Parse(ans) - double.Parse(textBox6.Text)) < clearance)
                {
                    ans = textBox6.Text;
                }

                string function = string.Format(
                               "{0}{1}(({2}{3}{4}){5}{6})",
                               item.intFirst.CustomToString(), item.caculate12, item.intSecond.CustomToString(),
                                item.caculate23, item.intThired.CustomToString(), item.caculate34, item.intFourth.CustomToString());

                additionAns.Add(
                            new AnsAndFunction()
                            {
                                Ans = ans.ToString(),
                                Function = function,
                                ListShow = item.sort + "-->" + function + "=" + ans,
                                strFirst = item.strFirst,
                                caculate12 = item.caculate12,
                                strSecond = item.strSecond,
                                caculate23 = item.caculate23,
                                strThired = item.strThired,
                                caculate34 = item.caculate34,
                                strFourth = item.strFourth,
                            });
            }
            #endregion

            #region//1/(2-(3/2))
            foreach (var item in AnsList2)
            {
                string last = api.Caculate(item.strThired, item.strFourth, item.caculate34).ToString();
                string midl = api.Caculate(item.strSecond, last, item.caculate23).ToString();
                string ans = api.Caculate(item.strFirst, midl, item.caculate12).ToString();
                if (System.Math.Abs(double.Parse(ans) - double.Parse(textBox6.Text)) < clearance)
                {
                    ans = textBox6.Text;
                }

                string function = string.Format(
                               "{0}{1}({2}{3}({4}{5}{6}))",
                                item.intFirst.CustomToString(), item.caculate12, item.intSecond.CustomToString(),
                                item.caculate23, item.intThired.CustomToString(), item.caculate34, item.intFourth.CustomToString());

                additionAns.Add(
                            new AnsAndFunction()
                            {
                                Ans = ans.ToString(),
                                Function = function,
                                ListShow = item.sort + "-->" + function + "=" + ans,
                                strFirst = item.strFirst,
                                caculate12 = item.caculate12,
                                strSecond = item.strSecond,
                                caculate23 = item.caculate23,
                                strThired = item.strThired,
                                caculate34 = item.caculate34,
                                strFourth = item.strFourth,
                            });
            }
            #endregion

            foreach (var item in additionAns)
            {
                listBox2.Items.Add((object)(item.ListShow));
            }

            AnsList2.AddRange(additionAns);
        }      
    }
}
