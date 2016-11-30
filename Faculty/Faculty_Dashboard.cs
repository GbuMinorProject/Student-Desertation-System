using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Caching;
using System.Net;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using Excel = Microsoft.Office.Interop.Excel;
//using System.Windows.Forms;
namespace Faculty
{
    public partial class Faculty_Dashboard : Form
    {

        String nm = null;
     //   String murlf = "http://localhost/Student_Desigatation/";
          String murlf = "http://172.25.5.54/student_desigatation/";
        String typeOfdesertation = "";
        String misc_marks = "";
        String report_marks = "";
        String technical_marks = "";
        String presentation_marks = "";
        String total_marks = "";
        String roll_no = "";
        static String fac_idf = "";
        public Faculty_Dashboard(String nam)
        {
            
            InitializeComponent();
            
            nm = nam;
            fac_idf = nam;
            WebClient wr = new WebClient();
            NameValueCollection nrec = new NameValueCollection();
            nrec.Add("fact_id", nam);
            
            byte[] resp= wr.UploadValues(murlf + "retr_fac_name", nrec);
            String serres = Encoding.UTF8.GetString(resp);
            fac_name[] facnam = JsonConvert.DeserializeObject<fac_name[]>(serres);
            welcom.Text = "Welcome "+facnam[0].Faculty_Name;
            Faculty_Pannel fcp = new Faculty_Pannel();
            fcp.Visible = false;
         //   MessageBox.Show("Dashboad");
            String urlst = murlf+"stun_under_faculty";
            WebClient we = new WebClient();
            NameValueCollection dat = new NameValueCollection();
            dat.Add("facult_id", nam);
            byte[] bt = we.UploadValues(urlst,dat);
            String rep = Encoding.UTF8.GetString(bt);
            stu_data_ser[] js = JsonConvert.DeserializeObject<stu_data_ser[]>(rep);
            
            int stu_len = js.Length;
            int i = 0;
            while (i<stu_len)
            {
                comboBox1.Items.Insert(i, js[i].roll_no);//+"  "+ js[i].name );
                //MessageBox.Show(js[i].name);
                comboBox2.Items.Insert(i, js[i].roll_no);
                comboBox3.Items.Insert(i, js[i].roll_no);
                comboBox4.Items.Insert(i, js[i].roll_no);
                i++;
            }
           

            Faculty_Pannel frm = new Faculty_Pannel();
            frm.Visible = false;
            // MessageBox.Show(nam);
            
            werror = new System.Windows.Forms.ErrorProvider();
            // String url = "http://gubappwebservices.esy.es/out_of_faculty";
            String url = murlf + "out_of_faculty";
            WebClient wc1 = new WebClient();
            NameValueCollection form = new NameValueCollection();
            form.Add("fact_id", nam);
            byte[] otherstudents = wc1.UploadValues(url, form);
            String othrstud = Encoding.UTF8.GetString(otherstudents);
            OtherStudent[] jsa = JsonConvert.DeserializeObject<OtherStudent[]>(othrstud);
            //int stud_len = jsa.Length;            
            int j = 0;
            while (j < jsa.Length)
            {
                Roll_comboBox.Items.Insert(j, jsa[j].Roll_no);
                j++;
            }
        }
         

        private void Faculty_Dashboard_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "My Student";
            tabPage2.Text = "Evaluation";
            WebClient we = new WebClient();
          //  MessageBox.Show("Dashboard");
            Faculty_Pannel fcp = new Faculty_Pannel();
            fcp.Visible = false;
            fcp.Close();
           
            //fcp.Show()
            //fcp.Show();
            //Faculty_Pannel.Visible = false;
            NameValueCollection nform = new NameValueCollection();
            nform.Add("fac_id", nm);
            String urls = murlf+"student_data_retr_dir_faculty";
            byte[] data = we.UploadValues(urls, nform);
            String resp = Encoding.UTF8.GetString(data);
            //MessageBox.Show(resp);
            // JavaScriptSerializer jser = new JavaScriptSerializer();
            //   List<stu_data_ser> ls = new List<stu_data_ser>();
            stu_data_ser[] s_der = JsonConvert.DeserializeObject<stu_data_ser[]>(resp);
            int lengt = s_der.Length;
 //           MessageBox.Show("length of array " + lengt.ToString());
            int i = 0;
            while (lengt > i)
            {
                ListViewItem lvti = new ListViewItem(s_der[i].name);
                //MessageBox.Show(s_der[0].name);
                lvti.SubItems.Add(s_der[i].roll_no);
                //             MessageBox.Show(s_der[1].roll_no);

                lvti.SubItems.Add(s_der[i].branch);
                //           MessageBox.Show(s_der[1].branch);

                lvti.SubItems.Add(s_der[i].topic);
                //         MessageBox.Show(s_der[0].topic);

                lvti.SubItems.Add(s_der[i].broad_area);
                //     MessageBox.Show(s_der[1].broad_area);
                listView1.Items.Add(lvti);
                i++;
            }


        }
        
        
        private void tabPage1_Click(object sender, EventArgs e)
        {
          
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

     /*   private void button1_Click_1(object sender, EventArgs e)
        {

            Evaulation ec = new Evaulation(nm);
            ec.Show();
            //    Evaulation e
        }*/

        private void button2_Click(object sender, EventArgs e)
        {
            Student frm = new Student();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String url = murlf+"change_major";
            String r_numb = comboBox1.Text;
          //  String ruim=r_numb.
            String topic = newbroad.Text;
            WebClient we = new WebClient();
            NameValueCollection ncc = new NameValueCollection();
            ncc.Add("roll_no", r_numb);
            ncc.Add("topic", topic);
            byte[] reet = we.UploadValues(url, ncc);
            String respon = Encoding.UTF8.GetString(reet);
            JavaScriptSerializer js = new JavaScriptSerializer();
            stu_resp rp = js.Deserialize<stu_resp>(respon);
           // JavaScriptSerializer js=nre 
           // MessageBox.Show(r_numb);
            if(rp.response_code==100)
            {
                MessageBox.Show("Updated Successfully", "Updated");

                //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXxx

                String Category = "Faculty";
                String Action = "Broad Area";
                String by = fac_idf;           

                String urle = murlf + "log_add";
                WebClient wc11 = new WebClient();
                NameValueCollection value = new NameValueCollection();

                value.Add("category", Category);
                value.Add("action", Action);
                value.Add("by_w", by);
                byte[] stud_resp = wc11.UploadValues(urle, value);
                //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX





            }
            else
                if(rp.response_code==101)
            {
                MessageBox.Show("Updation Not Possible", "Sorry");
            }
         
            //  this.printDocument1.Print += new PrintPageEventHandler();
        }

        

        private void button2_Click_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            WebClient we = new WebClient();
            NameValueCollection nform = new NameValueCollection();
            nform.Add("fac_id", nm);
            String urls = murlf+"student_data_retr_dir_faculty";
            byte[] data = we.UploadValues(urls, nform);
            String resp = Encoding.UTF8.GetString(data);
            //MessageBox.Show(resp);
            // JavaScriptSerializer jser = new JavaScriptSerializer();
            //   List<stu_data_ser> ls = new List<stu_data_ser>();
            stu_data_ser[] s_der = JsonConvert.DeserializeObject<stu_data_ser[]>(resp);
            int lengt = s_der.Length;
            //           MessageBox.Show("length of array " + lengt.ToString());
            int i = 0;
            while (lengt > i)
            {
                ListViewItem lvti = new ListViewItem(s_der[i].name);
                //MessageBox.Show(s_der[0].name);
                lvti.SubItems.Add(s_der[i].roll_no);
                //             MessageBox.Show(s_der[1].roll_no);

                lvti.SubItems.Add(s_der[i].branch);
                //           MessageBox.Show(s_der[1].branch);

                lvti.SubItems.Add(s_der[i].topic);
                //         MessageBox.Show(s_der[0].topic);

                lvti.SubItems.Add(s_der[i].broad_area);
                //     MessageBox.Show(s_der[1].broad_area);
                listView1.Items.Add(lvti);
                i++;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            String url = murlf + "inter_marks";
            String ex = extype.Text;
            String interroll = comboBox2.Text;
            String intermarks = internamlMarksbox.Text;
            double im;
            Double.TryParse(intermarks, out im);
            if (im < 0 || im > 40)
            {
                werror.SetError(this.internamlMarksbox, "Only marks Between 0 to 40 allowed");
                internamlMarksbox.Focus();

            }
            else
            {
                werror.SetError(this.internamlMarksbox, "");
                werror.Clear();
                WebClient we = new WebClient();
                NameValueCollection incoll = new NameValueCollection();
                incoll.Add("student_id", interroll);
                incoll.Add("internal", intermarks);
                incoll.Add("typee", ex);
                byte[] res = we.UploadValues(url, incoll);
                String respon = Encoding.UTF8.GetString(res);
                MessageBox.Show(respon);
                JavaScriptSerializer js = new JavaScriptSerializer();
                internal_marks rp = js.Deserialize<internal_marks>(respon);
                // JavaScriptSerializer js=nre 
                // MessageBox.Show(r_numb);
                if (rp.responce_code == 100)
                {
                    MessageBox.Show("Updated Successfully", "Updated");
                    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXxx

                    String Category = "Faculty";
                    String Action = "Internal Marks";
                    String by = fac_idf;

                    String urle = murlf + "log_add";
                    WebClient wc11 = new WebClient();
                    NameValueCollection value = new NameValueCollection();

                    value.Add("category", Category);
                    value.Add("action", Action);
                    value.Add("by_w", by);
                    byte[] stud_resp = wc11.UploadValues(urle, value);
                    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX



                }
                else
                    if (rp.responce_code == 101)
                {
                    MessageBox.Show("Updation Not Possible", "Sorry");
                }
            }
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            String rollno = comboBox3.Text;
            String taskde = richTextBox1.Text;
            String datep= dateTimePicker1.Text;
            WebClient wec = new WebClient();
            NameValueCollection nec = new NameValueCollection();
            nec.Add("sroll", rollno);
            nec.Add("task_de", taskde);
                nec.Add("dead", datep);
            byte[] bt = wec.UploadValues(murlf + "task_assgin", nec);
            String ser = Encoding.UTF8.GetString(bt);
           // MessageBox.Show(ser);
            JavaScriptSerializer jser = new JavaScriptSerializer();
            stu_resp sttp = jser.Deserialize<stu_resp>(ser);  
            //      stu_resp[] strpp = JsonConvert.DeserializeObject<stu_resp[]>(ser);
            if(sttp.response_code == 100)
            {
                MessageBox.Show("Task Added", "Task Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXxx

                String Category = "Faculty";
                String Action = "Task Assign";
                String by = fac_idf;

                String urle = murlf + "log_add";
                WebClient wc11 = new WebClient();
                NameValueCollection value = new NameValueCollection();

                value.Add("category", Category);
                value.Add("action", Action);
                value.Add("by_w", by);
                byte[] stud_resp = wc11.UploadValues(urle, value);
                //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX




            }
            else
            {
                MessageBox.Show("Something Went Wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
             }
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            String rollno = comboBox4.Text;
            listView2.Items.Clear();
            WebClient wec = new WebClient();
            NameValueCollection nec = new NameValueCollection();
            nec.Add("rollno", rollno);
            
            byte[] rest= wec.UploadValues(murlf + "task_desc", nec);
            String mret = Encoding.UTF8.GetString(rest);
            stu_task[] stdta = JsonConvert.DeserializeObject<stu_task[]>(mret);
            int sizlis = stdta.Length;
            int slen = 0;
            while(slen<sizlis)
            {
                ListViewItem ls = new ListViewItem(stdta[slen].id);
                ls.SubItems.Add(stdta[slen].student_id);
                ls.SubItems.Add(stdta[slen].task);
                ls.SubItems.Add(stdta[slen].assaign_date);
                ls.SubItems.Add(stdta[slen].deadline);
                ls.SubItems.Add(stdta[slen].status);
                listView2.Items.Add(ls);
                slen++;
            }
            
        }
        private void print_detail()
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument pdco = new PrintDocument();
            pd.Document = pdco;
            pdco.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            DialogResult drs = pd.ShowDialog();
            if(drs==DialogResult.OK)
            {
                printDocument1.Print();
            }


        }
        public void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // PrintDocument pdr = new PrintDocument();
            //pdr.Print();
            Graphics gr = e.Graphics;
            Font fn = new Font("Courier New", 8);
            float fhgt = fn.GetHeight();
            int stx = 3;
            int sty = 3;
            int offst = 20;
            gr.DrawString("Stundet List", new Font("Impact", 10), new SolidBrush(Color.Black), stx, sty);
            WebClient wcc = new WebClient();
            String urll= murlf + "student_data_retr_dir_faculty";
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("fac_id", nm);
            byte[] ress= wcc.UploadValues(urll, nvc);
            String respp = Encoding.UTF8.GetString(ress);
            stu_data_ser[] sdres = JsonConvert.DeserializeObject<stu_data_ser[]>(respp);
          //  stu_data_ser[] sdres = JsonConvert.DeserializeObject<stu_data_ser[]>(respp);
            int strlen = sdres.Length;
            int k = 0;
           while(k<strlen)
            {
                String nam = (sdres[k].name).PadRight(10);
              String rolno=sdres[k].roll_no.PadRight(10);
              String sbran= sdres[k].branch.PadRight(10);
              String stopi=sdres[k].topic.PadRight(10);
              String broad_are=sdres[k].broad_area.PadRight(10);
                //     String guid=sdres[k].guide;
                String prout = nam + "" + rolno + "" + sbran + "" + stopi + "" + broad_are + "";// + guid;
 //               MessageBox.Show(sbran);
                gr.DrawString(prout,fn, new SolidBrush(Color.Black), stx+20, sty + offst);
                offst = offst + (int)fhgt+10;
                k++;
            }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            print_detail();

        }

        

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Items.Clear();
            String stuid = comboBox4.Text;
            WebClient wec = new WebClient();
            NameValueCollection task_id = new NameValueCollection();
            task_id.Add("student_roll", stuid);
            byte[] resp = wec.UploadValues(murlf + "student_task", task_id);
            String oresp = Encoding.UTF8.GetString(resp);
          //  MessageBox.Show(oresp);
            task_id_ass[] tasid = JsonConvert.DeserializeObject<task_id_ass[]>(oresp);
            int p = 0;
            while (p < tasid.Length)
            {
                comboBox5.Items.Add(tasid[p].id);
                p++;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            String sid = comboBox4.Text;
            String tid = comboBox5.Text;
            String sta = comboBox6.Text;
            WebClient wec = new WebClient();
            NameValueCollection nec = new NameValueCollection();
            nec.Add("student_roll", sid);
            nec.Add("task_id", tid);
            nec.Add("student_status", sta);
            byte[] resp = wec.UploadValues(murlf + "task_status", nec);
            String srresp = Encoding.UTF8.GetString(resp);
            stu_resp str = (new JavaScriptSerializer()).Deserialize<stu_resp>(srresp);
            if(str.response_code==100)
            {
                MessageBox.Show("Task Status Updated", "Updated", MessageBoxButtons.OK);

                //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXxx

                String Category = "Faculty";
                String Action = "Status";
                String by = fac_idf;

                String urle = murlf + "log_add";
                WebClient wc11 = new WebClient();
                NameValueCollection value = new NameValueCollection();

                value.Add("category", Category);
                value.Add("action", Action);
                value.Add("by_w", by);
                byte[] stud_resp = wc11.UploadValues(urle, value);
                //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX



            }
            else
            {
                MessageBox.Show("Task Status Updatation Failed", "Failed", MessageBoxButtons.OK);
            }

            


        }

        private void button10_Click(object sender, EventArgs e)
        {
            Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            if(xlapp==null)
            {
                MessageBox.Show("Excel not Installed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Excel.Workbook excelwor;
            Excel.Worksheet excelshet;
            object misValue = System.Reflection.Missing.Value;
            excelwor = xlapp.Workbooks.Add(misValue);
            excelshet = (Excel.Worksheet)excelwor.Worksheets.get_Item(1);
            excelshet.Cells[1, 1] = "Roll No";
            excelshet.Cells[1, 2] = "Name";
            excelshet.Cells[2, 1] = "13/ICS/041";
            excelshet.Cells[2, 2] = "Sachin";
            //  excelwor.SaveAs("C:\\sampleexcel.xls", Excel.XlFileFormat.xlWorkbookNormal);
            excelwor.SaveAs("f:\\csharp-Excel.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
           // xlWorkBook.Close(true, misValue, misValue);
            excelwor.Close(true, misValue, misValue);
          
            xlapp.Quit();
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            if(xlapp==null)
            {
                MessageBox.Show("Excel not Installed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Excel.Workbook excelbook;
            Excel.Worksheet excelsheet;
            object misValue = System.Reflection.Missing.Value;
            excelbook = xlapp.Workbooks.Add(misValue);
            excelsheet = (Excel.Worksheet)excelbook.Worksheets.get_Item(1);
            excelsheet.Cells[1, 1] = "Name";
            excelsheet.Cells[1, 2] = "Roll No";
            excelsheet.Cells[1, 3] = "Branch";
            excelsheet.Cells[1, 4] = "Topic";
            excelsheet.Cells[1, 5] = "Broad Area";

            WebClient wcc = new WebClient();
            String urll = murlf + "student_data_retr_dir_faculty";
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("fac_id", nm);
            byte[] ress = wcc.UploadValues(urll, nvc);
            String respp = Encoding.UTF8.GetString(ress);
            stu_data_ser[] sdres = JsonConvert.DeserializeObject<stu_data_ser[]>(respp);
            //  stu_data_ser[] sdres = JsonConvert.DeserializeObject<stu_data_ser[]>(respp);
            int strlen = sdres.Length;
            int k = 0;
            while (k < strlen)
            {
                int p = 1;
                String nam = (sdres[k].name);//.PadRight(10);
                String rolno = sdres[k].roll_no;//.PadRight(10);
                String sbran = sdres[k].branch;//.PadRight(10);
                String stopi = sdres[k].topic;//.PadRight(10);
                String broad_are = sdres[k].broad_area;//.PadRight(10);
                excelsheet.Cells[k + 2, p] = nam;
                excelsheet.Cells[k + 2, p+1] = rolno;
                excelsheet.Cells[k + 2, p+2] = sbran;
                excelsheet.Cells[k + 2, p + 3] = stopi;
                excelsheet.Cells[k + 2, p + 4] = broad_are;
                k++;
            }
            excelbook.SaveAs("f:\\Guided_Student.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            excelbook.Close(true,misValue,misValue);
            xlapp.Quit();



        }

        private void midSemesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mmlable.Text = "Literature Survey and Problem Identification (10)";
            evaluated_list.Columns[2].Text = "Literature Survey and Problem Identification";
            groupBox1.Show();
            typeOfdesertation = "mid1";
           
        }

        private void endSemesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mmlable.Text = "Planning and Research Methodology with Partial Implementation (10)";
           evaluated_list.Columns[2].Text = "Planning and Research Methodology with Partial Implementation";
           // MessageBox.Show(evaluated_list.Columns[2].Text);
            groupBox1.Show();
            typeOfdesertation = "end1";
          
        }

        private void midSemesterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mmlable.Text = "Literature Survey and Problem Identification (10)";
           evaluated_list.Columns[2].Text = "Literature Survey and Problem Identification";
            groupBox1.Show();
            typeOfdesertation = "mid2";
           
        }

        private void button13_Click(object sender, EventArgs e)
        {

            if (backgroundWorker.IsBusy)
            {
                MessageBox.Show("Already in progress");
            }
            else
            {
                backgroundWorker.RunWorkerAsync();

            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!roll_no.Equals("") && !report_marks.Equals("") && !technical_marks.Equals("") && !presentation_marks.Equals("") && !misc_marks.Equals("") && !total_marks.Equals(""))
            {
                werror.SetError(this.button13, "");
                werror.Clear();
                // roll_no = Roll_comboBox.Text;
                String remarks = Remarks_textbox.Text;
                String uri = murlf + "marks_evaluation";
                //   String uri = "http://gubappwebservices.esy.es/marks_evaluation";
                WebClient wc = new WebClient();
                NameValueCollection form = new NameValueCollection();
                form.Add("facult_id", fac_idf);
                form.Add("studen_id", roll_no);
                form.Add("report_marks", report_marks);
                form.Add("technical_marks", technical_marks);
                form.Add("presentation_marks", presentation_marks);
                form.Add("total", total_marks);
                form.Add("remark", remarks);
                form.Add("misc_marks", misc_marks);
                form.Add("type", typeOfdesertation);
                byte[] resp = wc.UploadValues(uri, form);
                String res_string = Encoding.UTF8.GetString(resp);
                JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                marks_json resp_json = js.Deserialize<marks_json>(res_string);
                if (resp_json.response_code == 100)
                {
                    //Response_label.ForeColor = Color.Green;
                    // Response_label.Text = "Marks Submitted Successfully";
                    MessageBox.Show("Marks Submitted Successfully!", "Marks Evaluation Successfull");
                    String Category = "Faculty";
                    String Action = "External Marks";
                    String by = fac_idf;

                    String urle = murlf + "log_add";
                    WebClient wc11 = new WebClient();
                    NameValueCollection value = new NameValueCollection();

                    value.Add("category", Category);
                    value.Add("action", Action);
                    value.Add("by_w", by);
                    byte[] stud_resp = wc11.UploadValues(urle, value);

                }
                else if (resp_json.response_code == 110)
                {
                    MessageBox.Show("Marks Updated Successfully!", "Marks Updation Successfull");
                    String Category = "Faculty";
                    String Action = "Update External Marks";
                    String by = fac_idf;

                    String urle = murlf + "log_add";
                    WebClient wc11 = new WebClient();
                    NameValueCollection value = new NameValueCollection();

                    value.Add("category", Category);
                    value.Add("action", Action);
                    value.Add("by_w", by);
                    byte[] stud_resp = wc11.UploadValues(urle, value);

                }
                else
                {
                    MessageBox.Show("Something went wrong...Please Try Again", "Marks Evaluation Failed");
                }
                // e.Result = res_string;

            }
            else
            {
                werror.SetError(this.button13, "Fill all the details");
            }
        }
        

        private void Report_textbox_Leave(object sender, EventArgs e)
        {
            report_marks = Report_textbox.Text;
            double rm;
            Double.TryParse(report_marks, out rm);
            if (rm < 0 || rm > 5)
            {
                werror.SetError(this.Report_textbox, "Only marks Between 0 to 5 allowed");
                Report_textbox.Focus();
            }
            else
            {
                werror.SetError(this.Report_textbox, "");
                werror.Clear();
            }
        }

        private void Technical_textbox_Leave(object sender, EventArgs e)
        {
            technical_marks = Technical_textbox.Text;
            double tm;
            Double.TryParse(technical_marks, out tm);
            if (tm < 0 || tm > 10)
            {
                werror.SetError(this.Technical_textbox, "Only marks Between 0 to 10 allowed");
                Technical_textbox.Focus();

            }
            else
            {
                werror.SetError(this.Technical_textbox, "");
                werror.Clear();
            }
        }

        private void Presentation_textbox_Leave(object sender, EventArgs e)
        {

            presentation_marks = Presentation_textbox.Text;
            double pm;
            Double.TryParse(presentation_marks, out pm);
            if (pm < 0 || pm > 5)
            {
                werror.SetError(this.Presentation_textbox, "Only marks Between 0 to 5 allowed");
                Presentation_textbox.Focus();
            }
            else
            {
                werror.SetError(this.Presentation_textbox, "");
                werror.Clear();
                report_marks = Report_textbox.Text;
                technical_marks = Technical_textbox.Text;
                presentation_marks = Presentation_textbox.Text;
                misc_marks = misc_textbox.Text;
                if (misc_marks.Equals("") || report_marks.Equals("") || technical_marks.Equals("") || presentation_marks.Equals(""))
                {
                    werror.SetError(this.total_textview, "Some marks field may be empty ");
                }
                else
                {
                    werror.SetError(this.total_textview, "");
                    werror.Clear();
                    double rm;
                    Double.TryParse(report_marks, out rm);
                    double tm;
                    Double.TryParse(technical_marks, out tm);
                    double mm;
                    Double.TryParse(misc_marks, out mm);
                    Double.TryParse(presentation_marks, out pm);
                    double total = mm+rm + tm + pm;
                    total_marks = total.ToString();
                    total_textview.Text = total_marks;
                }
            }
        }

        private void total_textview_Leave(object sender, EventArgs e)
        {

            double tm;
            Double.TryParse(total_marks, out tm);
            if (tm < 0 || tm > 10)
            {
                werror.SetError(this.total_textview, "Only marks Between 0 to 10 allowed");
                total_textview.Focus();
            }
            else
            {
                werror.SetError(this.total_textview, "");
                werror.Clear();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Roll_comboBox.Text = "Select Roll No.";
            werror.Clear();
            Roll_comboBox.Focus();
            name_textview.Text = "";
            class_textview.Text = "";
            Report_textbox.Clear();
            Technical_textbox.Clear();
            Presentation_textbox.Clear();
            misc_textbox.Clear();
            total_textview.Text = "";
            Remarks_textbox.Text = "";
            werror.SetError(this.Report_textbox, "");
            werror.SetError(this.Technical_textbox, "");
            werror.SetError(this.Presentation_textbox, "");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            marksheetbox.Show();
            // String url = "http://gubappwebservices.esy.es/student_evaluted";
            String url = murlf + "student_evaluted";
            WebClient wc1 = new WebClient();
            NameValueCollection form = new NameValueCollection();
            form.Add("fact_id", fac_idf);
            byte[] eval_stud = wc1.UploadValues(url, form);
            String evalstud_str = Encoding.UTF8.GetString(eval_stud);
            EvaluatedStudents[] ev_stud = JsonConvert.DeserializeObject<EvaluatedStudents[]>(evalstud_str);
            int lengt = ev_stud.Length;
            int i = 0;
            evaluated_list.Items.Clear();
            while (lengt > i)
            {
                String Sr = (i + 1).ToString();
                ListViewItem mylist = new ListViewItem(Sr);
                mylist.SubItems.Add(ev_stud[i].Student_id);                
                mylist.SubItems.Add(ev_stud[i].planning_impl_rest);
                mylist.SubItems.Add(ev_stud[i].Name);
                mylist.SubItems.Add(ev_stud[i].Report_Writting);
                mylist.SubItems.Add(ev_stud[i].Technical_Content);
                mylist.SubItems.Add(ev_stud[i].Presentaion);
                mylist.SubItems.Add(ev_stud[i].Total);
                evaluated_list.Items.Add(mylist);
                i++;
            }
        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void Roll_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // String url = "http://gubappwebservices.esy.es/Student_detail_retrive_faculty";
            String url = murlf + "Student_detail_retrive_faculty";
            roll_no = Roll_comboBox.Text;
            if (!roll_no.Equals("Select Roll No."))
            {
                WebClient wc1 = new WebClient();
                NameValueCollection value = new NameValueCollection();
                value.Add("roll_no", roll_no);
                value.Add("facult_id", fac_idf);
                byte[] stud_resp = wc1.UploadValues(url, value);
                String res_string = Encoding.UTF8.GetString(stud_resp);
                Student_json[] result = JsonConvert.DeserializeObject<Student_json[]>(res_string);
                // marks_exist[] me = JsonConvert.DeserializeObject<marks_exist[]>(res_string);
                if (result.Length > 0)
                {
                    //  String url2 = "http://gubappwebservices.esy.es/mark_already_exist";
                    String url2 = murlf + "mark_already_exist";
                    WebClient wc2 = new WebClient();
                    NameValueCollection value2 = new NameValueCollection();
                    value2.Add("facult_id", fac_idf);
                    value2.Add("studen_id", roll_no);
                    byte[] stud_resp2 = wc2.UploadValues(url2, value2);
                    String roll_alredy = Encoding.UTF8.GetString(stud_resp2);
                    JavaScriptSerializer js2 = new System.Web.Script.Serialization.JavaScriptSerializer();
                    //JavaScriptSerializer jd1=
                    marks_exist resp_json2 = js2.Deserialize<marks_exist>(roll_alredy);
                    if (resp_json2.response_code == 100)
                    {
                        String name = result[0].Name.ToString();
                        String cl = result[0].Branch.ToString();
                        name_textview.Text = name;
                        class_textview.Text = cl;
                    }
                    else if (resp_json2.response_code == 106)
                    {
                        // werror.SetError(this.Roll_textbox, "Already evaluatted");
                        //Roll_textbox.Focus();
                        DialogResult checkmsgbox = MessageBox.Show("Click on Ok to update marks", "Already evaluated", MessageBoxButtons.OKCancel);
                        if (checkmsgbox == DialogResult.OK)
                        {
                            String name = result[0].Name.ToString();
                            String cl = result[0].Branch.ToString();
                            name_textview.Text = name;
                            class_textview.Text = cl;
                        }
                        else
                        {
                            Roll_comboBox.Text = "Select Roll No.";
                            Roll_comboBox.Focus();
                        }
                    }
                }
                else
                {
                    werror.SetError(this.Roll_comboBox, "Student with this Roll No may have not registered ");
                    Roll_comboBox.Focus();
                }
            }
            else
            {
                Roll_comboBox.Focus();
                //   werror.SetError(this.Roll_textbox, "");
            }
        }

        private void misc_textbox_Leave(object sender, EventArgs e)
        {
            misc_marks = misc_textbox.Text;
            double mm;
            Double.TryParse(misc_marks, out mm);
            if (mm < 0 || mm > 10)
            {
                werror.SetError(this.misc_textbox, "Only marks Between 0 to 10 allowed");
                misc_textbox.Focus();

            }
            else
            {
                werror.SetError(this.misc_textbox, "");
                werror.Clear();
            }
        }
    }
    class fac_name
    {
      public String Faculty_Name { get; set; }
    }
    class task_id_ass
        {
        public String id;
         }
    class stu_data_ser
    {
        public String name;
        public String roll_no;
        public String branch;
        public String topic;
        public String broad_area;
        public String guide;
    } 
    class stu_task
    {
        public String id;
        public String student_id;
        public String task;
        public String assaign_date;
        public String deadline;
        public String status;
    }
    class stu_resp
    {
     public   int response_code;
    }
    class internal_marks
    {
        public int responce_code;
    }
    public class Student_json
    {
        public string Name { get; set; }
        public string Branch { get; set; }
        public string broad_area { get; set; }
    }
    public class marks_json
    {
        public int response_code { get; set; }
    }
    public class marks_exist
    {
        public int response_code { get; set; }
    }
    public class EvaluatedStudents
    {

        public string Student_id { get; set; }
        public string Name { get; set; }
        public string Report_Writting { get; set; }
        public string Technical_Content { get; set; }
        public string planning_impl_rest { get; set; }
        public string Presentaion { get; set; }
        public string Total { get; set; }

    }

    public class Rootobject
    {
        public OtherStudent[] Property1 { get; set; }
    }

    public class OtherStudent
    {
        public string Roll_no { get; set; }

    }
}

