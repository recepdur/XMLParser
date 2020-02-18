using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLParser
{
    class Program
    {
        static void Main(string[] args)
        {
            // read
            string path = Directory.GetCurrentDirectory() + "/db.xml";
            Member member = GetXmlData(path);
            Customer cus = member.CustomerList.Find(x => x.customer_no == "recep" && x.password == "123456");
            Console.WriteLine(cus.public_key);

            // write
            Customer newCus = new Customer();
            newCus.customer_no = "yeni müşteri";
            newCus.public_key = "ppppppp";
            member.CustomerList.Add(newCus);
            SetXmlData(member, path);
        }

        public static Member GetXmlData(string path)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Member));
            TextReader reader = new StreamReader(path);
            object obj = deserializer.Deserialize(reader);
            Member member = (Member)obj;
            reader.Close();
            return member;
        }

        public static void SetXmlData(Member mem, string path)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Member));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                ser.Serialize(fs, mem);
            }
        }
    }

    public class Member
    {
        public List<Customer> CustomerList { get; set; }
    }

    public class Customer
    {
        public string id;
        public string customer_no;
        public string password;
        public string public_key;
    }
}
