PYTHON ?= python3.8
PIP := ${PYTHON} -m pip
DEPENDENCIES := sqlalchemy Faker psycopg2
EXE_FILE := ./fill_data.py
RUN_EXE := ${PYTHON} ${EXE_FILE}
ARGS ?=

all: install_deps run

install_deps:
	${PIP} install ${DEPENDENCIES}

help:
	${RUN_EXE} --help

run:
	${RUN_EXE} ${ARGS}
