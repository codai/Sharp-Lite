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
                        bag.Key(key => {
                            key.Column("ProductCategoryFk");
                        });
                        bag.Table("Products_ProductCategories");
                        bag.Cascade(Cascade.None);
                    },
                    collectionRelation =>
                        collectionRelation.ManyToMany(m => m.Column("ProductFk"))));

            
            /*
            // Initially the SubCategories and Parent were NOT mapped.
            mapper.Class<ProductCategory>(map => map.Set(x => x.SubCategories,
                                            set => {
                                                set.Key(k => k.Column("ParentCategoryId"));
                                                set.Inverse(true);
                                            },
                                                    ce => ce.OneToMany()));

            mapper.Class<ProductCategory>(map => map.ManyToOne(x => x.Parent,
                                            manyToOne => {
                                                manyToOne.Column("ParentCategoryId");
                                                manyToOne.Lazy(LazyRelation.NoLazy);
                                                manyToOne.NotNullable(false);
                                            }));
            */
        }
    }
}
