lxc delete -f main-silo
lxc profile set main-silo user.user-data - < main-silo.yml
lxc launch ubuntu: main-silo -p main-silo

