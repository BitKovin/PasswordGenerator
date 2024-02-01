namespace PasswordGenerator
{
    public partial class Form1 : Form
    {

        int length = 6;

        bool capital = false;

        bool useNumber = false;

        bool useSpecial = false;

        public Form1()
        {
            InitializeComponent();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int old = length;


            try
            {
                length = int.Parse(textBox1.Text);

                if (length <= 3)
                {
                    length = old;
                    textBox1.Text = old.ToString();
                    MessageBox.Show("password must be at least 4 symbol long");
                }

            }
            catch
            {
                textBox1.Text = old.ToString();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            capital = checkBox3.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            useSpecial = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            useNumber = checkBox2.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Generate();
        }

        void Generate()
        {
            List<string> chars = new List<string>();

            List<string> lower = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            List<string> upper = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            List<string> nums = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "8" };

            List<string> symbols = new List<string>() { "!", "@", "#", "$", "%", "^", "&", "*", "_", "-", "+", "=", "~", "`", "|", "", "/", "(", ")", "{", "}", "[", "]", ":", ";", "'", ",", ".", "<", ">", "?" };

            chars.AddRange(lower);

            if (capital)
                chars.AddRange(upper);

            if (useNumber)
                chars.AddRange(nums);

            if (useSpecial)
                chars.AddRange(symbols);

            string pass = "";

            Random rand = new Random();

            for (int i = 0; i < length; i++)
            {
                int n = rand.Next(chars.Count - 1);

                pass = pass + chars[n];
            }

            if (ContainsElement(pass, lower) == false)
            {
                Console.WriteLine("requred lower symbol not found");
                Generate();
                return;
            }


            if (capital)
                if (ContainsElement(pass, upper) == false)
                {
                    Console.WriteLine("requred capital symbol not found");
                    Generate();
                    return;
                }


            if (useNumber)
                if (ContainsElement(pass, nums) == false)
                {
                    Console.WriteLine("requred number not found");
                    Generate();
                    return;
                }


            if (useSpecial)
                if (ContainsElement(pass, symbols) == false)
                {
                    Console.WriteLine("requred symbol not found");
                    Generate();
                    return;
                }

            textBox2.Text = pass;
            button2.Enabled = true;
        }

        bool ContainsElement(string arr, List<string> elements)
        {
            foreach (string element in elements)
            {
                if (arr.Contains(element))
                    return true;
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Save();
        }

        void Save()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
            saveFileDialog1.Title = "Save a Password File";
            saveFileDialog1.FileName = "password.txt";

            DialogResult result = saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Save the content to the selected file
                string filePath = saveFileDialog1.FileName;
                SaveContentToFile(filePath, textBox2.Text);
                Console.WriteLine("File saved successfully at: " + filePath);
            }
        }

        void SaveContentToFile(string filePath, string content)
        {

            try
            {
                File.WriteAllText(filePath, content);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
