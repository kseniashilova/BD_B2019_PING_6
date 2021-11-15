from rest_framework import serializers
from .models import Reader, Book, Publisher, Category, Copy, Borrowing, BookCat


class ReaderSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Reader
        fields = ['number', 'last_name', 'first_name', 'address', 'birth_date']


class BookSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Book
        fields = ['isbn', 'title', 'author', 'pages_num', 'pub_year',
                  'publisher']


class PublisherSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Publisher
        fields = ['pub_name', 'pub_address']


class CategorySerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Category
        fields = ['category_name', 'parent_cat']


class CopySerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Copy
        fields = ['book', 'copy_number', 'shelf_position']


class BorrowingSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Borrowing
        fields = ['book_copy', 'shelf_position']


class BookCatSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = BookCat
        fields = ['book', 'category']
