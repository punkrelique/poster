#!/bin/bash

while read -r release; do
	helm uninstall "$release"
done < <(helm list --short)
