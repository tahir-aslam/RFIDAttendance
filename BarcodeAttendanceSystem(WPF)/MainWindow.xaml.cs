﻿using BarcodeAttendanceSystem_WPF_.DAL;
using BarcodeAttendanceSystem_WPF_.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Scenario.GSMSMSEngine;
using System.Windows.Threading;

namespace BarcodeAttendanceSystem_WPF_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InstituteDAL instituteDAL;
        StudentDAL studentDAL;
        MiscDAL miscDAL;

        Institute institue;
        Student student;
        string textInput = "";
        int count = 0;

        List<Student> AllStudentList;
        List<StudentAttendance> AllAttendanceList;
        List<StudentAttendance> insertAttendanceList;
        StudentAttendance sa;
        bool check = false;
        public static string m_SessionID = "6";
        SMSEngine m_SMSEngine;
        DispatcherTimer refreshDataTimer;
        RfidDAL rfidDAL;
        public MainWindow()
        {
            InitializeComponent();

            //this.InitializeComponent();
            //CoreWindow.GetForCurrentThread().KeyDown += MyPage_KeyDown;
            instituteDAL = new InstituteDAL();
            studentDAL = new StudentDAL();
            miscDAL = new MiscDAL();
            rfidDAL = new RfidDAL();
            try
            {
                institue = instituteDAL.GetInstitute();
                this.DataContext = institue;
                date_TB.Text = DateTime.Now.ToString("dd-MMMM-yyy");
                AllStudentList = studentDAL.get_all_admissions();
                absentAllStudents();

                m_SMSEngine = new SMSEngine();

                StartRefreshTimer();
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                msgGRID.Visibility = Visibility.Visible;
                msgTB.Text = ex.ToString();
            }
        }

        void StartRefreshTimer()
        {
            refreshDataTimer = new DispatcherTimer();
            refreshDataTimer.Interval = new TimeSpan(0, 0, 2);
            refreshDataTimer.Tick += refreshDataTimer_Tick;
            refreshDataTimer.Start();
        }

        void refreshDataTimer_Tick(object sender, EventArgs e)
        {
            v_TotalSmsSent.Text = m_SMSEngine.m_TotalSmsSent.ToString();
            v_TotalSmsQueued.Text = m_SMSEngine.m_SmsNos.Count().ToString();
        }

        void absentAllStudents()
        {
            // Absent all studends
            insertAttendanceList = new List<StudentAttendance>();
            AllAttendanceList = studentDAL.getAttendanceByDate(DateTime.Now);
            foreach (var adm in AllStudentList)
            {
                check = false;
                foreach (var att in AllAttendanceList.Where(x => x.std_id == adm.id))
                {
                    check = true;
                }
                if (check == false)
                {
                    sa = new StudentAttendance();
                    sa.std_id = adm.id;
                    sa.std_name = adm.std_name;
                    sa.class_id = adm.class_id;
                    sa.section_id = adm.section_id;
                    sa.date_time = DateTime.Now;
                    sa.created_by = "RFIDAttendanceAdmin";
                    sa.roll_no = adm.roll_no;
                    sa.attendence_date = DateTime.Now;
                    sa.attendence = 'A';

                    insertAttendanceList.Add(sa);
                }
            }

            if (studentDAL.insertAllStudentAttendance(insertAttendanceList) > 0)
            {

                msgGRID.Visibility = Visibility.Collapsed;
            }
            else
            {
                Debug.WriteLine("All Student Absent Not Inserted");
                msgTB.Text = "All Student Absent Not Inserted";
                msgGRID.Visibility = Visibility.Visible;
            }

        }

        void makeAttendance()
        {

        }

        void getStudent()
        {
        }

        private void listview1_Tapped(object sender, MouseButtonEventArgs e)
        {
            var source = e.OriginalSource as FrameworkElement;
            if (source == null) return;

            var item = source.DataContext as Student;
            if (item == null) return;

            // do stuff with your item
        }


        private void listview1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void listview1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine(e.Key.ToString());
            string input = e.Key.ToString();

            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrEmpty(textInput) && !string.IsNullOrWhiteSpace(textInput))
                {
                    try
                    {
                        rfid_assignment rfidObj = rfidDAL.GetIDFromRfidCArdNo(textInput);
                        if (rfidObj != null)
                        {
                            if (rfidObj.is_std == "Y")
                            {
                                student = studentDAL.getStudentInfo(rfidObj.card_holder_id);
                            }
                            else
                            {
                                //employee
                            }


                            if (student != null && student.std_name != null)
                            {
                                StudentAttendance sa = new StudentAttendance();

                                sa.std_id = student.id;
                                sa.std_name = student.std_name;
                                sa.class_id = student.class_id;
                                sa.section_id = student.section_id;
                                sa.date_time = DateTime.Now;
                                sa.created_by = "AttendanceAdmin";
                                sa.roll_no = student.roll_no;
                                sa.attendence_date = DateTime.Now;
                                sa.attendence = 'P';

                                if (studentDAL.insertStudentAttendance(sa) > 0)
                                {
                                    SMSQueue queue = new SMSQueue()
                                    {
                                        receiver_id = Convert.ToInt32(student.id),
                                        receiver_name = student.std_name,
                                        receiver_cell_no = student.cell_no,
                                        receiver_type_id = 1,
                                        sms_message = "Respected Parents," + Environment.NewLine + "AoA," + Environment.NewLine + student.std_name + " has entered in school at " + DateTime.Now.ToString("hh:mm tt")+Environment.NewLine+"On "+DateTime.Now.ToString("dd-MMM-yy")+"."  + Environment.NewLine + "Admin " + institue.institute_name + "." + Environment.NewLine + institue.institute_phone + Environment.NewLine + institue.institute_cell,
                                        sms_type = "RFID Attendance SMS",
                                        sms_type_id = 9,
                                        created_by = "admin",
                                        date_time = DateTime.Now,
                                        emp_id = 0,
                                        sort_order = 1
                                    };


                                    if (miscDAL.insertIntoQueue(queue) > 0)
                                    {
                                        count++;
                                        student.count = count;
                                        StudentSP.DataContext = student;
                                        studentProfileSP.DataContext = student;
                                        listview1.Items.Insert(0, student);
                                        msgGRID.Visibility = Visibility.Collapsed;
                                    }
                                    else
                                    {
                                        Debug.WriteLine("Attendence queue Not Inserted");
                                        msgTB.Text = "Attendance queue Not Inserted";
                                        msgGRID.Visibility = Visibility.Visible;
                                    }
                                }
                                else
                                {
                                    Debug.WriteLine("Attendance Not Inserted");
                                    msgTB.Text = "Attendance Not Inserted";
                                    msgGRID.Visibility = Visibility.Visible;
                                }
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Attendance Not Inserted");
                            msgTB.Text = "Incorrect Login";
                            msgGRID.Visibility = Visibility.Visible;
                        }
                        textInput = "";
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        msgGRID.Visibility = Visibility.Visible;
                        msgTB.Text = ex.ToString();
                    }
                }
            }
            else
            {
                if (input.Contains("Number"))
                {
                    input = input.Substring(6);
                    textInput = textInput + input;
                }
                if (input.Contains("D"))
                {
                    input = input.Substring(1);
                    textInput = textInput + input;
                }
                if (input.Equals(189))
                {
                    input = "-";
                    textInput = textInput + input;
                }

            }
        }
    }
}