<template>
   <div class="board-background">
    <!-- Top Controls -->
    <div class="top-controls">
     <!-- Enhanced Dropdown for Provider Selection -->
     <div class="provider-wrapper">
  <label class="provider-label">Select a Provider:</label>
  <div class="custom-dropdown">
    <div class="dropdown-selected" @click="toggleDropdown">
      <span>{{ selectedProvider }}</span>
      <i :class="isDropdownOpen ? 'arrow-up' : 'arrow-down'"></i>
    </div>
    <ul v-if="isDropdownOpen" class="dropdown-list">
      <li 
        v-for="provider in providers" 
        :key="provider.value" 
        @click="selectProvider(provider.value)"
        class="dropdown-item"
      >
        <i :class="provider.icon"></i>
        <span>{{ provider.name }}</span>
      </li>
    </ul>
  </div>
</div>

      <!-- Search Bar -->
      <div class="search-bar">
        <input 
          v-model="searchQuery" 
          @input="debouncedSearch" 
          placeholder="Search todos..."
        />
      </div>
    </div>

    <!-- Center Add Todo Input -->
    <div class="add-todo">
      <input v-model="newTodoTitle" placeholder="Enter a new todo" />
      <button @click="addTodo">Add Todo</button>
    </div>

    <!-- Sticky Notes Grid -->
    <div class="sticky-notes-grid">
      <TodoStickyNote
        v-for="todo in todos"
        :key="todo.id"
        :todo="todo"
        @delete-todo="deleteTodo"
        @toggle-complete="toggleComplete"
      />
    </div>
  </div>
</template>

<script>
import TodoStickyNote from '../components/TodoStickyNote.vue';
import axios from 'axios';
import { v4 as uuidv4 } from 'uuid';
import { debounce } from 'lodash';


export default {
    components: {
        TodoStickyNote
    },
    data() {
        return {
            todos: [],
      newTodoTitle: '',
      searchQuery: '',
      selectedProvider: 'MySql',
      isDropdownOpen: false,
      providers: [
        { name: 'MySQL', value: 'MySql', icon: 'icon-mysql' },
        { name: 'SQL Server', value: 'MSSQL', icon: 'icon-mssql' }
      ],
        };
    },
    methods: {
        toggleDropdown() {
      this.isDropdownOpen = !this.isDropdownOpen;
    },
    selectProvider(provider) {
      this.selectedProvider = provider;
      this.isDropdownOpen = false;
      this.fetchTodos();
    },
        async fetchTodos() {
            try {
                const response = await axios.get('https://localhost:8081/api/todos', {
                params: { provider: this.selectedProvider, search: this.searchQuery }
        });
                this.todos = response.data;
            } catch (error) {
                console.error("Error fetcching todos:", error);
            }
        },
        
        async addTodo() {
            if (this.newTodoTitle.trim() === '') return;
            const newTodo = {id: uuidv4(), title: this.newTodoTitle, isCompleted: false, source: this.selectedProvider};
            try {4
                const response = await axios.post(`https://localhost:8081/api/todos?provider=${this.selectedProvider}`, newTodo);
                this.todos.push(response.data);
                this.newTodoTitle = '';
            } catch (error) {
                console.error('Error adding todo:', error);
            }
        },
        
        async deleteTodo(id) {
            try {
                await axios.delete(`https://localhost:8081/api/todos/${id}?provider=MySql`);
                this.todos = this.todos.filter(todo => todo.id != id);
            } catch (error) {
                console.error('Error Deleting todo:', error);
            }
        },

        async toggleComplete(todo) {
            try {
                const updateTodo = { ...todo, isCompleted: !todo.isCompleted };
                await axios.put(`https://localhost:8081/api/todos?provider=MySql`, updateTodo);
                this.todos = this.todos.map(t => (t.id === todo.id ? updateTodo : t));
            } catch (error) {
                console.error('Error updating todo:', error);
            }
        },
        debouncedSearch: debounce(function () {
            this.fetchTodos();
        }, 300),
    },
    mounted() {
        this.fetchTodos();
    },
};
</script>

<style scoped>

html, body {
  margin: 0;
  padding: 0;
  height: 100%;
  overflow: hidden;
}

.board-background {
  background: radial-gradient(circle, #d7b38c 10%, #b18f6a 100%);
  width: 100%;
  height: 100vh;  /* Full viewport height */
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 20px;
  box-shadow: inset 0 0 50px rgba(0, 0, 0, 0.5);
}

.top-controls {
  display: flex;
  align-items: start;
  gap: 20px;
  margin-bottom: 20px;
}

.provider-wrapper {
  display: flex;
  align-items: center;
  gap: 10px;
}

.provider-label {
  font-weight: bold;
}

.custom-dropdown {
  position: relative;
  display: inline-block;
}

.dropdown-selected {
  display: flex;
  align-items: center;
  padding: 10px 15px;
  background-color: #fff;
  border: 1px solid #ccc;
  border-radius: 5px;
  cursor: pointer;
  width: 200px;
  justify-content: space-between;
}

.dropdown-selected i {
  margin-left: 10px;
}

.arrow-down::after {
  content: "▼";
}

.arrow-up::after {
  content: "▲";
}

.dropdown-list {
  position: absolute;
  top: 100%;
  left: 0;
  width: 200px;
  background-color: #fff;
  border: 1px solid #ccc;
  border-radius: 5px;
  overflow: hidden;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  z-index: 100;
}

.dropdown-item {
  padding: 10px 15px;
  display: flex;
  align-items: center;
  gap: 10px;
  cursor: pointer;
  transition: background 0.3s;
}

.dropdown-item:hover {
  background-color: #f0f0f0;
}

.icon-mysql::before {
  content: "🛢️"; /* Icon for MySQL */
}

.icon-mssql::before {
  content: "💾"; /* Icon for SQL Server */
}

.search-bar {
  display: flex;
  align-items: center;
}

.search-bar input {
  padding: 10px 15px;
  width: 250px;  /* Match the width of the dropdown */
  border: 1px solid #ccc;
  border-radius: 5px;
}

.add-todo {
  display: flex;
  justify-content: center;
  margin-bottom: 20px;
}

.add-todo input {
  padding: 10px;
  width: 300px;
  margin-right: 10px;
}

.add-todo button {
  padding: 10px;
  cursor: pointer;
}

.sticky-notes-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 15px;
  justify-content: flex-start;
  align-items: flex-start;
  width: 100%;
  height: 100%;
  overflow-y: auto;
}
</style>