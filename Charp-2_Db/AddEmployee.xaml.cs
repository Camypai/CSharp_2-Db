using System;
using System.Windows;
using System.Windows.Controls;
using Charp_2_Db.Models;

namespace Charp_2_Db
{
    public partial class AddEmployee
    {
        /// <summary>
        /// Контект для работы с БД
        /// </summary>
        private readonly JobDbContext _db;

        /// <summary>
        /// Id созданного сотрудника
        /// </summary>
        public int? Id = null;
        
        public AddEmployee(JobDbContext db)
        {
            InitializeComponent();

            _db = db;
        }

        /// <summary>
        /// Создание сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            var departmentReference = (Department) ComboBox.SelectedItem;
            
            var emp = new Employee
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                MiddleName = MiddleName.Text,
                Age = int.Parse(Age.Text),
                DepartmentId = departmentReference.Id,
                Experience = int.Parse(Experience.Text),
                Position = Position.Text
            };
            
            Id = _db.EmployeeRepository.Create(emp);

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
