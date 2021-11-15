# from django.shortcuts import render
from .models import Reader, Book, Publisher, Category, Copy, \
                    Borrowing, BookCat
from .serializers import ReaderSerializer, BookSerializer, \
                         PublisherSerializer, CategorySerializer, \
                         CopySerializer, BorrowingSerializer, BookCatSerializer
from rest_framework import viewsets


# Create your views here.
class ReaderViewSet(viewsets.ModelViewSet):
    queryset = Reader.objects.all()
    serializer_class = ReaderSerializer
    permission_classes = []


class BookViewSet(viewsets.ModelViewSet):
    queryset = Book.objects.all()
    serializer_class = BookSerializer
    permission_classes = []


class PublisherViewSet(viewsets.ModelViewSet):
    queryset = Publisher.objects.all()
    serializer_class = PublisherSerializer
    permission_classes = []


class CategoryViewSet(viewsets.ModelViewSet):
    queryset = Category.objects.all()
    serializer_class = CategorySerializer
    permission_classes = []


class CopyViewSet(viewsets.ModelViewSet):
    queryset = Copy.objects.all()
    serializer_class = CopySerializer
    permission_classes = []


class BorrowingViewSet(viewsets.ModelViewSet):
    queryset = Borrowing.objects.all()
    serializer_class = BorrowingSerializer
    permission_classes = []


class BookCatViewSet(viewsets.ModelViewSet):
    queryset = BookCat.objects.all()
    serializer_class = BookCatSerializer
    permission_classes = []
