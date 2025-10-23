using BusinessObjects;
using Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProductManagementDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ICatergoryService iCategoryService;
        private readonly IProductService iProductService;

        public MainWindow()
        {
            InitializeComponent();
            iCategoryService = new CategoryService();
            iProductService = new ProductService();
        }

        public void LoadCategories()
        {
            try
            {
                var categories = iCategoryService.GetCategories();
                cboCategory.ItemsSource = categories;
                cboCategory.DisplayMemberPath = "CategoryName";
                cboCategory.SelectedValuePath = "CategoryID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void LoadProducts()
        {
            try
            {
                var products = iProductService.GetProducts();
                dgData.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                resetInput();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategories();
            LoadProducts();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product();
                product.ProductName = txtProductName.Text;
                product.UnitPrice = decimal.Parse(txtPrice.Text);
                product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                product.CategoryID = Int32.Parse(cboCategory.SelectedValue.ToString());
                IProductService productService = new ProductService();
                productService.SaveProduct(product);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadProducts();
            }
        }

        //private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    DataGrid dataGrid = sender as DataGrid;
        //    DataGridRow row =
        //    (DataGridRow)dataGrid.ItemContainerGenerator
        //    .ContainerFromIndex(dataGrid.SelectedIndex);
        //    DataGridCell RowColumn =
        //    dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
        //    string id = ((TextBlock)RowColumn.Content).Text;
        //    Product product = iProductService.GetProductByID(Int32.Parse(id));
        //    txtProductID.Text = product.ProductID.ToString();
        //    txtProductName.Text = product.ProductName;
        //    txtPrice.Text = product.UnitPrice.ToString();
        //    txtUnitsInStock.Text = product.UnitsInStock.ToString();
        //    cboCategory.SelectedValue = product.CategoryID;
        //}

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product selected = dgData.SelectedItem as Product;
            if (selected == null) return;

            txtProductID.Text = selected.ProductID.ToString();
            txtProductName.Text = selected.ProductName;
            txtPrice.Text = selected.UnitPrice.ToString();
            txtUnitsInStock.Text = selected.UnitsInStock.ToString();
            cboCategory.SelectedValue = selected.CategoryID;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product();
                product.ProductID = Int32.Parse(txtProductID.Text);
                product.ProductName = txtProductName.Text;
                product.UnitPrice = decimal.Parse(txtPrice.Text);
                product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                product.CategoryID = Int32.Parse(cboCategory.SelectedValue.ToString());
                iProductService.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadProducts();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtProductID.Text.Length > 0)
                {
                    Product product = new Product();
                    product.ProductID = Int32.Parse(txtProductID.Text);
                    product.ProductName = txtProductName.Text;
                    product.UnitPrice = decimal.Parse(txtPrice.Text);
                    product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                    product.CategoryID = Int32.Parse(cboCategory.SelectedValue.ToString());
                    iProductService.DeleteProduct(product);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadProducts();
            }

        }

        private void resetInput()
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            txtPrice.Text = "";
            txtUnitsInStock.Text = "";
            cboCategory.SelectedValue = 0;
        }
    }

    
}