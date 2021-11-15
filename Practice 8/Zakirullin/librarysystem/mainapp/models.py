from django.db import models


# Create your models here.
class Reader(models.Model):
    number = models.IntegerField(primary_key=True)
    last_name = models.CharField(max_length=30)
    first_name = models.CharField(max_length=30)
    address = models.CharField(max_length=200)
    birth_date = models.DateField()


class Book(models.Model):
    isbn = models.CharField(max_length=17, primary_key=True)
    title = models.CharField(max_length=200)
    author = models.CharField(max_length=100)
    pages_num = models.IntegerField()
    pub_year = models.IntegerField()
    publisher = models.ForeignKey('Publisher', on_delete=models.PROTECT)


class Publisher(models.Model):
    pub_name = models.CharField(max_length=100, primary_key=True)
    pub_address = models.CharField(max_length=200)


class Category(models.Model):
    category_name = models.CharField(max_length=50, primary_key=True)
    parent_cat = models.ForeignKey('Category', null=True,
                                   on_delete=models.SET_NULL)


class Copy(models.Model):
    book = models.ForeignKey('Book', primary_key=True,
                             on_delete=models.PROTECT)
    copy_number = models.IntegerField(primary_key=True)
    shelf_position = models.CharField(max_length=20)


class Borrowing(models.Model):
    reader = models.ForeignKey('Reader', primary_key=True,
                               on_delete=models.CASCADE)
    book_copy = models.ForeignKey('Copy', primary_key=True,
                                  on_delete=models.CASCADE)
    # Don't we need to allow multiple borrowings of the same copy by the same
    # reader?
    return_date = models.DateField()


class BookCat(models.Model):
    book = models.ForeignKey('Book', primary_key=True,
                             on_delete=models.CASCADE)
    category = models.ForeignKey('Category', primary_key=True,
                                 on_delete=models.CASCADE)
