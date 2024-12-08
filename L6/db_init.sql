use WSRAM;

create table Student(
  id int not null identity(1,1) primary key,
  name nvarchar(max) not null
)

create table Note(
  id int not null identity(1,1) primary key,
  subj nvarchar(max) not null,
  note int not null,
  student_id int not null references student(id) on delete cascade
)

insert into Student (name) values ('Lioksa');
insert into Student (name) values ('Ilja');
insert into Student (name) values ('Hanna');

insert into Note (student_id, subj, note) values (1, 'Maths', 10);
insert into Note (student_id, subj, note) values (1, 'Philosophy', 9);
insert into Note (student_id, subj, note) values (1, 'DB', 9);

insert into Note (student_id, subj, note) values (2, 'Maths', 8);
insert into Note (student_id, subj, note) values (2, 'Philosophy', 10);
insert into Note (student_id, subj, note) values (2, 'DB', 9);

insert into Note (student_id, subj, note) values (3, 'Maths', 9);
insert into Note (student_id, subj, note) values (3, 'Philosophy', 8);
insert into Note (student_id, subj, note) values (3, 'DB', 10);
