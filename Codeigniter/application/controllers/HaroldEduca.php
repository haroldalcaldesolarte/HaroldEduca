<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class HaroldEduca extends CI_Controller {

	public function __construct()
	{
		parent::__construct();

		$this->load->database();
		$this->load->helper('url');

		$this->load->library('grocery_CRUD');
	}

	public function _example_output($output = null)
	{
		$this->load->view('haroldEduca.php',(array)$output);
	}

	public function categoria()
	{
		$output = $this->grocery_crud->render();

		$this->_example_output($output);
	}

	public function categoria_management()
	{
		try{
			$crud = new grocery_CRUD();

			$crud->set_theme('datatables');
			$crud->set_relation('id_cat_padre','categoria','nombre_cat');
			$crud->display_as('nombre_cat','Categoria');
			$crud->display_as('id_cat_padre','Categoria Padre');
			$crud->set_table('categoria');
			$crud->set_subject('Categoria');
			$crud->required_fields('nombre_cat');
			$crud->columns('nombre_cat','id_cat_padre');

			$output = $crud->render();

			$this->_example_output($output);

		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}

	public function pregunta_management()
	{
		try{
			$crud = new grocery_CRUD();

			$crud->set_theme('datatables');
			$crud->set_relation('id_categoria','categoria','{nombre_cat}');
			$crud->display_as('id_categoria','Categoria');
			$crud->set_table('pregunta');
			$crud->set_subject('Pregunta');
			$crud->required_fields('descripcion','dificultad','id_categoria');
			$crud->columns('descripcion','id_categoria','dificultad');

			$output = $crud->render();

			$this->_example_output($output);

		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}

	public function respuesta_management()
	{
		try{
			$crud = new grocery_CRUD();

			$crud->set_theme('datatables');
			$crud->display_as('id_preg','Pregunta');
			$crud->display_as('correcta','Respuesta correcta');
			$crud->set_relation('id_preg','pregunta','descripcion');
			$crud->set_table('respuesta');
			$crud->set_subject('Respuesta');
			$crud->required_fields('descripcion','correcta','id_pregunta');
			$crud->columns('descripcion','id_preg','correcta');

			$output = $crud->render();

			$this->_example_output($output);

		}catch(Exception $e){
			show_error($e->getMessage().' --- '.$e->getTraceAsString());
		}
	}

		public function index()
	{
		$this->_example_output((object)array('output' => '' , 'js_files' => array() , 'css_files' => array()));
	}
}