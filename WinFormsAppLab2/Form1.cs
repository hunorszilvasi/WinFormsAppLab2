using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace WinFormsAppLab2

{
    
    public partial class Form1 : Form
    {
        private static BooleanSwitch logSwitch =
    new BooleanSwitch("LogSwitch", "Enable logging");
        private MyThread myThread;
        public Form1()
        {
            InitializeComponent();

            myThread = new MyThread(MoveLabel);

            Trace.Listeners.Add(
                new TextWriterTraceListener("log.txt")
            );

            Trace.AutoFlush = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            myThread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myThread.Stop();
        }

        private void MoveLabel()
        {
            Invoke(new Action(() =>
            {
                label1.Left += 10;

                if (label1.Left > Width)
                    label1.Left = 0;
            }));

            if (logSwitch.Enabled)
            {
                Trace.WriteLine(
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                );
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myThread.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = colorDialog1.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.ForeColor = colorDialog1.Color;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private List<Person> persons = new List<Person>();

        private void button6_Click(object sender, EventArgs e)
        {
            Person person = new Person(
                textBox1.Text,
                textBox2.Text,
                dateTimePicker1.Value
            );

            persons.Add(person);

            listBox1.Items.Add(person);

            textBox1.Clear();
            textBox2.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Person person = (Person)listBox1.SelectedItem;

            MessageBox.Show(
                $"{person.Name} kora: {person.GetAge()} év"
            );
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        public Person(string name, string surname, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }

        public int GetAge()
        {
            int age = DateTime.Now.Year - BirthDate.Year;

            if (DateTime.Now < BirthDate.AddYears(age))
            {
                age--;
            }

            return age;
        }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }
}
