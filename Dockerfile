############################################################
#
# This builds a docker image and runs the demo scripts to
# enable testing on a linux environment
#
# Usage: 
#   1. Change the Connection strings to reference host.docker.internal for a local (host) instance of SQL Server:
#       frosting/build/program.cs
#       script/build.cake
#   2. Build the docker image:
#       "docker build -t cake.grate ."
#   3. Run the docker image with interactive flag
#       "docker run -it cake.grate"
#   4. Call the run.sh script
#       "../demo/run.sh"
#
############################################################


FROM cakebuild/cake:sdk-8.0-alpine-v4.0.0 AS builder

ADD ./src  /src/
ADD ./demo /demo/
