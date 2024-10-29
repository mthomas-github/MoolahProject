<template>
   <div
    class="sticky-note"
    :class="{ completed: todo.isCompleted }"
  >
    <!-- Complete Icon -->
    <button class="complete-icon" @click="toggleComplete">
      <i class="fas fa-check"></i>
    </button>

    <h3>{{ todo.title }}</h3>

    <!-- Delete Icon -->
    <button class="delete-icon" @click="$emit('delete-todo', todo.id)">
      <i class="fas fa-trash"></i>
    </button>
  </div>
</template>

<script>
export default {
    props: {
        todo: {
            type: Object,
            required: true,
        },
    },
    methods: {
        toggleComplete() {
            this.$emit('toggle-complete', this.todo);
        },
    },
};
</script>

<style scoped>
.sticky-note {
    color: black;
  width: 200px;
  height: 200px;
  background-color: #fef68a; /* Default sticky note color */
  margin: 10px;
  padding: 15px;
  border: 2px solid #fcd34d;
  border-radius: 10px;
  box-shadow: 3px 3px 8px rgba(0, 0, 0, 0.2), -3px -3px 8px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  transition: transform 0.2s, background-color 0.3s;
  cursor: grab;
  position: relative;
}

.sticky-note.complete {
    background-color: #d1fae5;
}

.sticky-note:hover {
  transform: rotate(-2deg) scale(1.05);
}

button {
    border: none;
    cursor: pointer;
    background: none;
}

.mark-complete {
    color: #059669;
}

.complete-icon,
.delete-icon {
  position: absolute;
  background: none;
  border: none;
  cursor: pointer;
  font-size: 1.2rem;
}

.complete-icon {
  top: 5px;
  left: 5px;  /* Position at the top-left corner */
  color: #059669;  /* Green for incomplete */
  transition: color 0.3s;
}

.delete-icon {
  top: 5px;
  right: 5px;  /* Position at the top-right corner */
  color: red;
}

.sticky-note.completed {
  background-color: #4caf50;  /* Green when completed */
  border-color: #388e3c;
}

.sticky-note.completed .complete-icon {
  color: white;  /* White icon for completed */
}

</style>