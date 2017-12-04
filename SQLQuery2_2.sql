select * from Movement where ProductID = NULL

select ProductID, sum(BoxUnits) boxes, sum(FractionUnits) fractions from Movement group by ProductID

update Product set ActiveFlag = 0 where Description like 'fideo%'

select * from product where ActiveFlag = 1;
update product set activeflag = 0 where id = 547

select * from Movement

select * from PurchasePlanDetail where PurchasePlanID =  2088