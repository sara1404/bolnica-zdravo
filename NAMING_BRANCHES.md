# Branches naming convention

## Branch types

`main` - only production ready code goes here, make sure to merge everything from develop to `main` right before control point 
`develop` - merge the `bugfix`, `refactor` and `hotfix` branches here while the code is still in development phase
`feature` - implement new features here, then merge to `develop`
`hotfix` - you can merge this branch type to `main` for a fix that needs to be addressed quickly **please use this branch carefully**
`refactor` - any code refactoring goes here
`bugfix` - solve bugs here than merge to `develop`

## Regex
To check if your branch name complies with this convention go to regex101.com and use this regex to check:
`(\bbugfix\b|\bhotfix\b|\bfeature\b)\/([A-Za-z0-9\-]+)`
`main`, `develop`, `refactor` branches are not to be renamed, i.e. you don't need to add the `/abc...` suffix to them
Please be sure that the **whole string** matches!