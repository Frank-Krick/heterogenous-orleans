lxc delete -f orleans-postgres
lxc profile set orleans-postgres user.user-data - < postgres-grain-storage.yml
lxc launch ubuntu: orleans-postgres -p orleans-postgres

