using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PryPistoliaGinoCRUD_ADO.NET
{
    internal class Program
        
    {
        static string connectionString = "Server = localhost\\SQLEXPRESS; Database = Comercio; Trusted_Connection = True";
        static SqlConnection conn;

        static void Main(string[] args)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1 - Create");
                Console.WriteLine("2 - Read");
                Console.WriteLine("3 - Update");
                Console.WriteLine("4 - Delete");
                Console.WriteLine("0 - Exit");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": Create(); break;

                    case "2": Read(); break;

                    case "3": Update(); break;

                    case "4": Delete(); break;

                    case "0": return;
                    default: Console.WriteLine("La opcion seleccionada no es posible."); break;
                }

                Console.WriteLine("\nPor favor oprima cualquier tecla para continuar");
                Console.ReadKey();
            }
        }


        static void Create()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                Console.Write("Nombre: ");
                Console.WriteLine("Descripcion: ");
                Console.WriteLine("Precio: ");
                Console.WriteLine("Stock: ");
                Console.WriteLine("Categoriaid: ");

                string nombre = Console.ReadLine();
                string descripcion = Console.ReadLine();
                decimal precio = Convert.ToDecimal(Console.ReadLine());
                int stock = Convert.ToInt32(Console.ReadLine());
                decimal categoriaId = Convert.ToDecimal(Console.ReadLine());

                string insertQuery = "INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, CategoriaId) VALUES (@nombre, @descripcion, @precio, @stock, @categoriaId)";
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@precio", precio);
                cmd.Parameters.AddWithValue("@stock", stock);
                cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Producto agregado con éxito.");
            }
        }

        static void Read()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                String Query = "SELECT P.Nombre, P.Precio, P.Stock, C.Nombre AS Categoria FROM Productos P JOIN Categorias C ON P.CategoriaId = C.Id";

                SqlCommand cmd = new SqlCommand(Query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Nombre"]} - ${reader["Precio"]} - Stock: {reader["Stock"]} - Categoría: {reader["Categoria"]}");
                }
                reader.Close();
            }
        }

        static void Update()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                Console.Write("Nombre: ");
                Console.WriteLine("nuevoPrecio: ");

                string nuevoNombre = Console.ReadLine();
                decimal nuevoPrecio = Convert.ToDecimal(Console.ReadLine());
                string updateQuery = "UPDATE Productos SET Precio = @precio WHERE Nombre = @nombre";

                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@precio", nuevoPrecio);
                cmd.Parameters.AddWithValue("@nombre", nuevoNombre);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Producto actualizado.");
            }
        }

        static void Delete()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string deleteQuery = "DELETE FROM Productos WHERE Nombre = @nombre";
                SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                cmd.Parameters.AddWithValue("@nombre", "Nombre");
                cmd.ExecuteNonQuery();
                Console.WriteLine("Producto eliminado.");
            }
        }
    }
}

   
