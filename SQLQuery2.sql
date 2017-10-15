select * from correlative 

update correlative set activeflag = 1

delete Document where Discriminator = 'SaleDocument'
