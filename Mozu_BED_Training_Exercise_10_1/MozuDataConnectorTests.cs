using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mozu.Api;
using Autofac;
using Mozu.Api.ToolKit.Config;
using System.Collections.Generic;

namespace Mozu_BED_Training_Exercise_10_1
{
    [TestClass]
    public class MozuDataConnectorTests
    {
        private IApiContext _apiContext;
        private IContainer _container;

        [TestInitialize]
        public void Init()
        {
            _container = new Bootstrapper().Bootstrap().Container;
            var appSetting = _container.Resolve<IAppSetting>();
            var tenantId = int.Parse(appSetting.Settings["TenantId"].ToString());
            var siteId = int.Parse(appSetting.Settings["SiteId"].ToString());

            _apiContext = new ApiContext(tenantId, siteId);
        }

        [TestMethod]
        public void Exercise_10_1_Get_Product_Type()
        {
            //Create a new ProductType resource
            var productTypeResource = new Mozu.Api.Resources.Commerce.Catalog.Admin.Attributedefinition.ProductTypeResource(_apiContext);

            //Now that you're more familiar with using the Mozu API documentation, I'll just give you the link for the ProductType JSON information
            //http://developer.mozu.com/content/api/APIResources/commerce/commerce.catalog/commerce.catalog.admin.attributedefinition.producttypes.htm

            //Add Your Code: 
            //Get list of attributes with max page size and starting index at the beginning
            var productTypes = productTypeResource.GetProductTypesAsync(startIndex: 0, pageSize: 200).Result;

            //Add Your Code: 
            //Write total count of producttypes to output window
            System.Diagnostics.Debug.WriteLine(string.Format("Product Type Total Count: {0}", productTypes.TotalCount));

            //Add Your Code: 
            //Get product type filtered by name 
            var typePurse = productTypeResource.GetProductTypesAsync(filter: "Name sw 'Purse'").Result.Items[0];

            //Loop through List<AttributeInProductType>
            foreach (var option in typePurse.Options)
            {
                //Loop through List<AttributeVocabularyValueInProductType>
                foreach (var value in option.VocabularyValues)
                {
                    //Add Your Code: 
                    //Write vocabulary values to output window
                    System.Diagnostics.Debug.WriteLine(string.Format("Product Type [{0}]: Value({1}) StringValue({2})",
                        typePurse.Name, value.Value, value.VocabularyValueDetail.Content.StringValue));
                }
            }
        }
    }
}
