using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShoeShopManagement.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string MD5Hash(string str)
        {
            StringBuilder hash = new StringBuilder();
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(new UTF8Encoding().GetBytes(str));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("X2"));
            }
            return hash.ToString();
        }
        public long ConvertToNumber(string str)
        {
            string[] s = str.Split(',');
            string tmp = "";
            foreach (string a in s)
            {
                tmp += a;
            }
            return long.Parse(tmp);
        }
        public int ConvertToColor(string str)
        {
            if(str == "Xanh")
            {
                return 1;
            }
            else if (str == "Đỏ")
            {
                return 2;
            }
            else if (str == "Tím")
            {
                return 3;
            }
            else if (str == "Vàng")
            {
                return 4;
            }
            else if (str == "Nâu")
            {
                return 5;
            }
            else if (str == "Đen")
            {
                return 6;
            }
            else if (str == "Trắng")
            {
                return 7;
            }
            else
            return 0;
        }

        public int ConvertToType(string str)
        {
            if(str == "Sneaker Nike")
            {
                return 1;
            }
            if (str == "Adidas")
            {
                return 2;
            }
            if (str == "Vans")
            {
                return 3;
            }
            if (str == "Balenciaga")
            {
                return 4;
            }
            if (str == "Converse")
            {
                return 5;
            }
            if (str == "Puma")
            {
                return 6;
            }
            if (str == "Pitis Hunter")
            {
                return 7;
            }
            return 0;
        }
        public string ConvertIntToColor(string str)
        {
            if (str == "1")
            {
                return "Xanh";
            }
            if (str == "2")
            {
                return "Đỏ";
            }
            if (str == "3")
            {
                return "Tím";
            }
            if (str == "4")
            {
                return "Vàng";
            }
            if (str == "5")
            {
                return "Nâu";
            }
            if (str == "6")
            {
                return "Đen";
            }
            if (str == "7")
            {
                return "Trắng";
            }
            return null;
        }
        public int ConvertToUnit(string str)
        {
            if(str == "Đôi")
            {
                return 1;
            }   
            else if(str == "Cái")
            {
                return 2;
            }
            else
            return 0;
        }
        public int ConvertToSize(int str)
        {
            if (str == 38)
            {
                return 1;
            }
            else if (str == 39)
            {
                return 2;
            }
            else if (str == 40)
            {
                return 3;
            }
            else if (str == 41)
            {
                return 4;
            }
            else if (str == 36)
            {
                return 5;
            }
            else return 0;
        }
        public string ConvertIntToSize(string str)
        {
            if (str == "1")
            {
                return "38";
            }
            else if (str == "2")
            {
                return "39";
            }
            else if (str == "3")
            {
                return "40";
            }
            else if (str == "4")
            {
                return "41";
            }
            else if (str == "5")
            {
                return "36";
            }
            else if(str== "6")
            {
                return "37";
            }    
            else return null;
        }
        public void SeparateThousands(TextBox txt)
        {
            if (!string.IsNullOrEmpty(txt.Text))
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                ulong valueBefore = ulong.Parse(ConvertToNumber(txt.Text).ToString(), System.Globalization.NumberStyles.AllowThousands);
                txt.Text = String.Format(culture, "{0:N0}", valueBefore);
                txt.Select(txt.Text.Length, 0);
            }
        }
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


    }
}
