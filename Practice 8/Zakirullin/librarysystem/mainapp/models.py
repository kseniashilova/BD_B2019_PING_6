from django.db import models


# Create your models here.
class Reader(models.Model):
    # number = models.IntegerField(primary_key=True)  # Doesn't seem to work
    # # with django_seed if there are foreign keys refering to this model
    number = models.AutoField(primary_key=True)
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
    book = models.ForeignKey('Book', on_delete=models.PROTECT)
    copy_number = models.IntegerField()
    shelf_position = models.CharField(max_length=20)

    class Meta:
        constraints = [models.UniqueConstraint(fields=['book', 'copy_number'],
                       name='copy_composite_workaround_uk')]


class Borrowing(models.Model):
    reader = models.ForeignKey('Reader', on_delete=models.CASCADE)
    book_copy = models.ForeignKey('Copy', on_delete=models.CASCADE)
    # Don't we need to allow multiple borrowings of the same copy by the same
    # reader?
    return_date = models.DateField()

    class Meta:
        constraints = [models.UniqueConstraint(fields=['reader', 'book_copy'],
                       name='borrowing_composite_workaround_uk')]


class BookCat(models.Model):
    book = models.ForeignKey('Book', on_delete=models.CASCADE)
    category = models.ForeignKey('Category', on_delete=models.CASCADE)

    class Meta:
        constraints = [models.UniqueConstraint(fields=['book', 'category'],
                       name='bookcat_composite_workaround_uk')]
