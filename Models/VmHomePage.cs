
namespace LapShop.Models
{
    public class VmHomePage
    {

        public VmHomePage()
        {
            lstAllItems = new List<VwItem>();
            lstRecommendedItem = new List<VwItem>();
            lstNewItems = new List<VwItem>();
            lstFreeDelivryItmes = new List<VwItem>();
            lstCategories = new List<TbCategory>();
            lstSliders = new List<TbSlider>();

        }
        public List<VwItem> lstAllItems { get; set; }
        public List<VwItem> lstRecommendedItem { get; set; }
        public List<VwItem> lstNewItems { get; set; }
        public List<VwItem> lstFreeDelivryItmes { get; set; }
        public List<TbCategory> lstCategories { get; set; }

        public List<TbSlider> lstSliders { get; set; }
        public TbSettings Settings { get; set; }
        

    }
}
