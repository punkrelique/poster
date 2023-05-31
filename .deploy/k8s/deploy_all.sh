#!/bin/bash

helm install backend ./helm/backend

helm install db ./helm/db

helm install frontend ./helm/frontend

helm install ingress ./helm/ingress

