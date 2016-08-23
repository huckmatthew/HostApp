FROM centos-with-mono

EXPOSE 9000

WORKDIR /opt/appcode

ENTRYPOINT /usr/bin/mono HostApp.exe
