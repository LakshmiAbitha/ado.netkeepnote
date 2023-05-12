using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace ado.netkeepnote
{
    class Note
    {
        public void createnote(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from note", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            var row = ds.Tables[0].NewRow();
            Console.WriteLine("Enter the note title");
            row["title"]=Console.ReadLine();
            Console.WriteLine("Enter the  note description");
            row["descriptions"] = Console.ReadLine();
            Console.WriteLine("Enter the note date");
            row["dates"] = Console.ReadLine();
            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Database inserted");
        }
        public void viewnote(SqlConnection con)
        {
            Console.WriteLine("Enter the id to view");
            int id=Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter adp=new SqlDataAdapter($"select * from note where id={id}",con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "notetable");
            for (int i = 0; i < ds.Tables["notetable"].Rows.Count; i++)
            {
                Console.WriteLine("id | title| description | date");
                for (int j = 0; j < ds.Tables["notetable"].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables["notetable"].Rows[i][j]} | ");
                }
                Console.WriteLine();
            }
        }
        public void viewallnote(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from note", con);
            DataSet ds = new DataSet();
            adp.Fill(ds,"notetable");
            for (int i = 0; i < ds.Tables["notetable"].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables["notetable"].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables["notetable"].Rows[i][j]} | ");
                }
                Console.WriteLine();
            }
        }
        public void updatenote(SqlConnection con)
        {
            Console.WriteLine("Enter the id");
            int id=Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"select * from note where id={id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            Console.WriteLine("Enter the column name to update");
            string column=Console.ReadLine();
            Console.WriteLine("Enter the row to update");
            int index=Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter the updated value");
            string value=Console.ReadLine();
            ds.Tables[0].Rows[index][column] = value;
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Note updated successfully");
        }
        public void deletenote(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from note", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            Console.WriteLine("Enter the row to delete");
            int row=Convert.ToInt16(Console.ReadLine());
            ds.Tables[0].Rows[row].Delete();
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Delete succesfully");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Server=IN-6H3K9S3; database=keepnotedb; Integrated Security=true");
            Note obj = new Note();
            string ans = "";
            do
            {
                Console.WriteLine(" Welcome to the keepnote");
                Console.WriteLine("1. create note");
                Console.WriteLine("2. view note by id ");
                Console.WriteLine("3. view all note");
                Console.WriteLine("4. update note");
                Console.WriteLine("5. delete note");
                Console.WriteLine("Enter your choice");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            obj.createnote(con);
                            break;
                        }
                    case 2:
                        {
                            obj.viewnote(con);
                            break;
                        }
                    case 3:
                        {
                            obj.viewallnote(con);
                            break;
                        }
                    case 4:
                        {
                            obj.updatenote(con);
                            break;
                        }
                    case 5:
                        {
                            obj.deletenote(con);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("you have entered Invalid choice");
                            break;
                        }
                }
                Console.WriteLine("Do you wish to continue[y/n]");
                ans = Console.ReadLine();
            } while (ans.ToLower() == "y");
        }
    }
}