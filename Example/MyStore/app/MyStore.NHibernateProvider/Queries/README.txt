This folder contains any concrete, NHibernate-specific query classes.
There should only be classes in here for any respective query *interfaces* found in 
/MyStore.Domain/Queries/

This folder will usually be empty except for very exceptive cases.

For larger projects, you may want to organize queries to reflect the domain's namespace breakdown; e.g.,:

/Queries
/ProductMgmt/Queries

When starting a project, agree as a team how you'll organize these folders.