using MyStore.Domain.ProductMgmt;
using NHibernate.Mapping.ByCode;

namespace MyStore.NHibernateProvider.Overrides
{
    internal class ProductCategoryOverride : IOverride
    {
        public void Override(ModelMapper mapper) {
            // Defines the ProductCategory side of the many-to-many relationship with Product
            mapper.Class<ProductCategory>(map =>
                map.Bag(x => x.Products,
                    bag => {
                        bag.Key(key => key.Column("ProductCategoryFk"));
                        bag.Table("Products_ProductCategories");
                        bag.Cascade(Cascade.None);
                    },
                    collectionRelation =>
                        collectionRelation.ManyToMany(m => m.Column("ProductFk"))));
        }
    }
}
