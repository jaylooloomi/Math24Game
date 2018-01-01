using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Math24.API;
using Math24.Model;

namespace Math24.View
{
    public partial class Math24Dialog : Form
    {
        public delegate double CaculateAction(double one, double two, out string str);       
        List<List<string>> obj { get; set; } = new List<List<string>>();
        List<CaculateAction> CaculateActionList { get; set; } = new List<CaculateAction>();
        Dictionary<string, int> dic = new Dictionary<string, int>();
        List<AnsAndFunction> correct = new List<AnsAndFunction>();
        List<AnsAndFunction> ProbabilitySortList  = new List<AnsAndFunction>();
     

        public Math24Dialog()
        {
            InitializeComponent();
        }

        private void Math24Dialog_Load(object sender, EventArgs e)
        {
            var api = MathAPI.GetInstance();
            var actionList = new List<CaculateAction>() { api.Addition, api.Subtraction, api.Multiplication, api.Division };
            CaculateActionList.AddRange(actionList);
        }       

        private void button1_Click(object sender, EventArgs e)
        {
            button2.PerformClick();

            var api = MathAPI.GetInstance();
            ((Button)sender).Enabled = false;
            textBox5.Text = "";
            this.Refresh();

            if (InitialDicPool(ref dic) == false)
            {
                //MessageBox.Show("請輸入有效字串，請勿輸入數字以外的字串!");
                MessageBox.Show("Error Insert!");
                ((Button)sender).Enabled = true;
                return;
            }
            ProbabilitySortList.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            var str = api.GetInputLine(dic);
            var strListNest = api.GetTptalSort(str, obj);
            var intListNest = api.GetIntListNest(strListNest, dic);


            foreach (var item in intListNest)
            {
                api.CaculateAnswer(api.CaculateAnswerGlobal(item), CaculateActionList, ProbabilitySortList, listBox2);
            }

            ShowAnswer(ref correct);
            if (oneAnsChecked.Checked == true && correct.Count != 0)
            {

            }
            else
            {
                //補齊可能缺少的加減法  例：(1+2)*(3+8) or 1+(2*3)+8
                api.CaculateAdditionAns(textBox6, listBox2, ref ProbabilitySortList);
                ShowAnswer(ref correct);
            }

            label10.Text = listBox2.Items.Count.ToString();
            label11.Text = correct.Count.ToString();

            ((Button)sender).Enabled = true;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {       
            //textBox1.Text = "";
            //textBox2.Text = "";
            //textBox3.Text = "";
            //textBox4.Text = "";

            textBox5.Text = "";
            listBox2.Items.Clear();
            listBox1.Items.Clear();
            label10.Text = "...";
            label11.Text = "...";
            textBox6.Text = "24";

            textBox1.Focus();

            dic.Clear();
            obj.Clear();
        }

        private void ShowAnswer(ref List<AnsAndFunction> correct2)
        {
            correct2.Clear();

            foreach (var item in ProbabilitySortList)
            {
                if (System.Math.Abs(double.Parse(item.Ans) - double.Parse(textBox6.Text)) < 0.000001)
                {
                    correct2.Add(item);
                }
            }

            if (correct.Count == 0)
            {
                textBox5.Text = "Can not find solution!";
            }
            else
            {
                textBox5.Text = correct2.First().ListShow;
                foreach (var item in correct2)
                {
                    listBox1.Items.Add(item.ListShow);
                }
            }
        }

        public bool InitialDicPool(ref Dictionary<string, int> dic2)
        {
            dic2.Clear();
            try
            {
                dic2.Add("a", int.Parse(textBox1.Text));
                dic2.Add("b", int.Parse(textBox2.Text));
                dic2.Add("c", int.Parse(textBox3.Text));
                dic2.Add("d", int.Parse(textBox4.Text));
                int condition = int.Parse(textBox6.Text);
            }
            catch (System.Exception ex)
            {

                return false;
            }

            return true;
        }
    }
}
