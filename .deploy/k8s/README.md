To start cluster locally run in minikube: <code>minikube tunnel</code>. Also, as it uses Ingress, <code>minikube addons enable ingress</code> should be executed<br/>
In yml file <code>ingress-controller.yml</code> there is host named my-poster-app.com and to make it work you need to:
- Linux: Add the result of <code>minikube ip</code> to <code>/etc/hosts</code> (for example, 192.168.1.42 my-poster.app)
- Windows: Add <code>127.0.0.1 my-poster.app</code>
