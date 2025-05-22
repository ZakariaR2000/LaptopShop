using LapShop.Models;

namespace LapShop.Bl
{

    public interface IPages
    {
        public List<TbPages> GetAll();
        public TbPages GetById(int id);
        public bool Save(TbPages ItemType);
        public bool Delete(int id);

    }
    public class ClsPages : IPages
    {
        LapShopContext context;

        public ClsPages(LapShopContext ctx)
        {
            context = ctx;
        }
        public List<TbPages> GetAll()
        {

            try
            {
                var lstCategories = context.TbPages.Where(a => a.CurrentState == 1).ToList();

                return lstCategories;
            }
            catch
            {
                return new List<TbPages>();
            }


        }


        
        public TbPages GetById(int id)
        {
            try
            {
                var itemType = context.TbPages.FirstOrDefault(a => a.PageId == id && a.CurrentState == 1);
                return itemType;
            }
            catch
            {
                return new TbPages();
            }
        }

        public bool Save(TbPages ItemType)
        {

            try
            {

                if (ItemType.PageId == 0)
                {
                    ItemType.CreatedBy = "1";
                    ItemType.CreatedDate = DateTime.Now;
                    context.TbPages.Add(ItemType);
                }
                else
                {
                    ItemType.UpdatedBy = "1";
                    ItemType.UpdatedDate = DateTime.Now;
                    context.Entry(ItemType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                context.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
            
        }

        public bool Delete(int id)
        {
            try
            {

                var ItemType = GetById(id);

                ItemType.CurrentState = 0;

                context.SaveChanges();

                return true;
            }
            catch
            {
                return false;

            }
        }



    }
}
