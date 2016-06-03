echo
echo '--------------------------------------------------'
echo $1

cd ../$1

echo git commit -m "'$2'"
git commit -m "'$2'"

cd ../paralib-net-reference

