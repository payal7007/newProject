using DemoProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Controllers
{
    public class HomeController : Controller
    {
        dataAccess data = new dataAccess();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ProductList(string SearchItem, int? i)
        {

            IEnumerable<MyAdvertiseModel> products = data.GetAllProductList();
            return View(products);
        }
        public ActionResult Details(int? advertiseId)
        {
            if (advertiseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var repo = new dataAccess();
            MyAdvertiseModel advertisement = repo.GetAdvertiseById(advertiseId);

            if (advertisement == null)
            {
                return HttpNotFound();
            }

            return View(advertisement);
        }
        [HttpGet]
        
        public ActionResult Edit(int id)
        {
            var repo = new dataAccess();
            // Retrieve the advertisement you want to edit by its ID
            MyAdvertiseModel advertisement = repo.GetAdvertiseById(id);

            if (advertisement == null)
            {
                return HttpNotFound();
            }

            return View(advertisement);
        }
        [HttpPost]

        public ActionResult Edit(MyAdvertiseModel advertisement)
        {
            if (ModelState.IsValid)
            {
                var repo = new dataAccess();
                bool updated = repo.UpdateAdvertisement(advertisement);

                if (updated)
                {
                    // Redirect to a success page or return a success message
                    return RedirectToAction("Success");
                }
                else
                {
                    // Handle update failure
                    ModelState.AddModelError("", "Failed to update the advertisement.");
                }
            }

            // If ModelState is not valid or update fails, return to the edit page with errors
            return View(advertisement);
        }
    }

    }
ALTER procedure [dbo].[GetSellerData]
as
begin
select tbl_MyAdvertise.advertiseId,tbl_MyAdvertise.productSubCategoryId,tbl_MyAdvertise.advertiseTitle,tbl_MyAdvertise.advertiseDescription,tbl_MyAdvertise.advertisePrice,
tbl_MyAdvertise.areaId,tbl_Area.areaName,tbl_MyAdvertise.advertiseStatus,tbl_MyAdvertise.UserId, tbl_MyAdvertise.advertiseapproved, tbl_MyAdvertise.createdOn ,tbl_MyAdvertise.updatedOn,
Users.firstName,tbl_AdvertiseImages.imageData from tbl_MyAdvertise
full join Users on tbl_MyAdvertise.userid = Users.userid
full join tbl_Area on tbl_MyAdvertise.areaId = tbl_Area.areaId
full join tbl_AdvertiseImages on tbl_MyAdvertise.advertiseId = tbl_AdvertiseImages.advertiseId
where tbl_MyAdvertise.advertiseId=tbl_MyAdvertise.advertiseId
end
ALTER PROCEDURE [dbo].[GetAdvertiseDetails]
@advertiseId INT
AS
BEGIN
SELECT a.advertiseId, psc.productSubCategoryName AS productSubCategoryName, a.advertiseTitle, a.advertiseDescription, a.advertisePrice, area.areaName, u.firstName, a.createdOn, a.updatedOn, i.imageData
FROM tbl_MyAdvertise a
LEFT JOIN tbl_AdvertiseImages i ON a.advertiseId = i.advertiseId
LEFT JOIN tbl_Area area ON a.areaId = area.areaId
LEFT JOIN tbl_ProductSubCategory psc ON a.productSubCategoryId = psc.productSubCategoryId
LEFT JOIN Users u ON u.userId = a.userId
WHERE a.advertiseId = @advertiseId
END;
USE [OLX_DB]
GO
/****** Object:  StoredProcedure [dbo].[spUpdatetbl_MyAdvertise]    Script Date: 10-10-2023 17:54:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spUpdatetbl_MyAdvertise]
@advertiseId int,
@productSubCategoryName nvarchar(50),
@advertiseTitle nvarchar(50),
@advertiseDescription nvarchar(100),
@advertisePrice decimal(18,2),
@areaName nvarchar(50),
@advertiseStatus bit,
@firstName nvarchar(50),
@advertiseapproved bit,
@createdOn datetime,
@updatedOn datetime,
@imageData varbinary(max)
AS
BEGIN
UPDATE spUpdatetbl_MyAdvertise
SET productSubCategoryName = @productSubCategoryName,
advertiseTitle = @advertiseTitle,
advertiseDescription = @advertiseDescription,
advertisePrice = @advertisePrice,
areaName = @areaName,
advertiseStatus = @advertiseStatus,
firstName = @firstName,
advertiseapproved = @advertiseapproved,
createdOn = @createdOn,
updatedOn = @updatedOn,
imageData = @imageData
WHERE advertiseId = @advertiseId
END

ALTER PROCEDURE [dbo].[DeleteAdvertise]
@advertiseId int
AS
BEGIN
DELETE FROM YourTableName
WHERE advertiseId = @advertiseId
END
