using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EF_Test
{
    class TestDbContext : DbContext
    {
        public TestDbContext() : base("server=localhost;port=3306;database=sample_db;uid=root;password=password")
        {
        }
        public DbSet<SampleTable> Sample { get; set; }
    }

    [Table("sample_table")]
    class SampleTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("sample_table_id")]
        public int Id { get; set; }

        [Column("sample_column")]
        public string SampleColumn { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Working();
            Failing("text");
        }

        static void Working()
        {
            var context = new TestDbContext();
            var result = from t in context.Sample where t.SampleColumn.Contains("text") select t;
            foreach (var s in result)
                Console.WriteLine(s.SampleColumn);
        }

        static void Failing(string search)
        {
            var context = new TestDbContext();
            var result = from t in context.Sample where t.SampleColumn.Contains(search) select t;
            foreach (var s in result)
                Console.WriteLine(s.SampleColumn);
        }
    }
}
