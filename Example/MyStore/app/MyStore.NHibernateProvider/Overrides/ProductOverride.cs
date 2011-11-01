using MyStore.Domain.ProductMgmt;
using NHibernate.Mapping.ByCode;

namespace MyStore.NHibernateProvider.Overrides
{
    internal class ProductOverride : IOverride
    {
        public void Override(ModelMapper mapper) {
            // Defines the Product side of the many-to-many relationship with ProductCategory
            mapper.Class<Product>(map => 
                map.Bag(x => x.Categories,
                    bag => {
                        bag.Key(key => {
                            key.Column("ProductFk");
                        });
                        bag.Table("Products_ProductCategories");
                        bag.Cascade(Cascade.None);
                    },
                    collectionRelation =>
                        collectionRelation.ManyToMany(m => m.Column("ProductCategoryFk"))));
        }
    }
}
