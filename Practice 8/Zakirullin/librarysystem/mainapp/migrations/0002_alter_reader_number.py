# Generated by Django 3.2.9 on 2021-12-02 22:39

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('mainapp', '0001_initial'),
    ]

    operations = [
        migrations.AlterField(
            model_name='reader',
            name='number',
            field=models.AutoField(primary_key=True, serialize=False),
        ),
    ]
