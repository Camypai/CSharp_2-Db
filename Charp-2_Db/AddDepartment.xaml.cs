using System.Windows;
using Charp_2_Db.Models;

namespace Charp_2_Db
{
    public partial class AddDepartment
    {
        /// <summary>
        /// Контект для работы с БД
        /// </summary>
        private readonly JobDbContext _db;

        /// <summary>
        /// Id созданного департамента
        /// </summary>
        public int? Id = null;
        
        public AddDepartment(JobDbContext db)
        {
            _db = db;
            InitializeComponent();
        }

        /// <summary>
        /// Создание департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dep = new Department
            {
                Name = Name.Text
            };
            
            _db.DepartmentRepository.Create(dep);
            _db.DepartmentRepository.Save();

            Id = dep.Id;

            DialogResult = true;
        }

        /// <summary>
        /// Отмена создания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
