######################################################
#
#   This file is to be run in the Docker image defined
#   in the repo root
#   Details are in the Dockerbuild file 
#
######################################################


#start up folder is home/src
cd ../demo/frosting
./build.sh
cd ../script
./build.sh