using DBPro.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DBPro.Test
{
    public class DBTest
    {
        public void Test()
        {
            testAccount();
            testItem();
            testItemAccusation();
            testItemCollection();
            testItemEvaluation();
            testItemTag();
            testOrder();
            testOrderItem();
            testPayInfo();
            testRefundInfo();
            testShop();
            testShopAccusation();
            testShopFollow();
            testShopItem();
            testShoppingCart();
            testShopTag();
            testUser();
            testUserInformation();
            testUserInteraction();
            testImage();
        }


        public void testAccount()
        {
            const int TEST_NUM = 1000;
            for(int i=0;i<TEST_NUM;++i)
            {
                int tem = i;
               Trace.Assert(  EntityRepositories.addObject(
                    new Account(i.ToString(), i.ToString(), tem.ToString(), DateTime.Now, DateTime.Now, tem.ToString())));
                tem *= 11;
                Trace.Assert(EntityRepositories.updateObject(
                    new Account(i.ToString(), i.ToString(), tem.ToString(), DateTime.Now, DateTime.Now, tem.ToString())));
            }
            Trace.Assert(EntityRepositories.getObject<Account>("select * from dbaccount").Count == TEST_NUM);
            Trace.Assert(EntityRepositories.getAllObject<Account>().Count == TEST_NUM);
            for (int i = 0; i < TEST_NUM; ++i)
            {
                int tem = i;
                Trace.Assert(EntityRepositories.delObject(
                    new Account(i.ToString(), i.ToString(), tem.ToString(), DateTime.Now, DateTime.Now, tem.ToString())));
            }
        }
        void testItem(){ }
        void testItemAccusation(){ }
        void testItemCollection() { }
        void testItemEvaluation() { }
        void testItemTag() { }
        void testOrder() { }
        void testOrderItem() { }
        void testPayInfo() { }
        void testRefundInfo() { }
        void testShop() { }
        void testShopAccusation() { }
        void testShopFollow() { }
        void testShopItem() { }
        void testShoppingCart() { }
        void testShopTag() { }
        void testUser() { }
        void testUserInformation() { }
        void testUserInteraction() { }
        void testImage() { }
    }
}
